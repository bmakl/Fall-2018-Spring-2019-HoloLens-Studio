using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTower : MonoBehaviour
{


    [Header("Turret Stats")]
    public float radius = 5f;
    public float attackSpeed = 1f;
    public float attackCountdown = 0f;
    public float attackDamage = 1f;
    public float health = 1f;
    public float turnSpeed = 12;


    [Header("Setup Stuff")]
    public string enemyTag = "Enemy";
    public Transform target;
    public Transform rotatingPart; //Assign part that will rotate to this

    public GameObject bulletPrefab;
    public Transform firePoint;

    public Queue<Transform> targetQueue;
    public HashSet<Transform> inQueueCheck;

    private float distanceToRadius;

    private Transform tempTarget;

    private void Awake()
    {
        targetQueue = new Queue<Transform>();
        inQueueCheck = new HashSet<Transform>();
    }

    private void Start()
    {
        target = null;
    }

    private void Update()
    {

        UpdateTarget();
        if (target == null) //If no target does nothing
        {
            return;
        }

        RotateTower();

        if (attackCountdown <= 0f) //checks if the attack is off cooldown 
        {

            //Shoot();

            attackCountdown = 1f / attackSpeed;
        }

        attackCountdown -= Time.deltaTime;

    }


    public void UpdateTarget() //Updates the target to the first target that enters the radius
    {
        Debug.Log("In Queue " + inQueueCheck.Count);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= radius)
            {
                Debug.Log(targetQueue.Count);
                Enqueue(enemy.transform);
            }
        }

        if (target == null)
        {
            if (targetQueue.Count > 0)
            {
                tempTarget = targetQueue.Dequeue();
                inQueueCheck.Remove(tempTarget);
                if (tempTarget == null)
                {
                    return;
                }

                float distanceToRadiusTemp = Vector3.Distance(transform.position, tempTarget.transform.position);

                if (distanceToRadiusTemp > radius)
                {
                    Enqueue(tempTarget);
                }


                else if (distanceToRadiusTemp <= radius)
                {
                    target = tempTarget;
                }
            }


        }
    }


    public void RotateTower() //Rotates tower to track target
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    public void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //creates the bullet and casts to Game Object
        Bullet bullet = bulletGO.GetComponent<Bullet>(); //

        if (bullet != null)
        {
            bullet.Seek(target, attackDamage); //passes the target to bullet script
        }
        else if (bullet == null)
        {
            Debug.LogError("Bullet does not exist");
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Enqueue(Transform enemy)
    {

        {
            if (inQueueCheck.Add(enemy))
            {
                targetQueue.Enqueue(enemy);
            }
        }
    }



}

