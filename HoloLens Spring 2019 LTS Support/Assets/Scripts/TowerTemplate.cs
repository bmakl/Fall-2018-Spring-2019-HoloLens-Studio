using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class TowerTemplate : MonoBehaviour, IInputClickHandler
{


    [Header("Turret Base Stats")]
    public float radius = 5f;
    public float attackSpeed = 1f;
    [HideInInspector]
    public float attackCountdown = 0f;
    public float attackDamage = 1f;
    public float health = 1f;
    public float turnSpeed = 12;

    [Header("Turret Upgrade 1 Stats")]
    public float upgrade1Damage;
    public float upgrade1AttackSpeed;
    public float upgrade1Range;

    [Header("Turret Upgrade 2 Stats")]
    public float upgrade2Damage;
    public float upgrade2AttackSpeed;
    public float upgrade2Range;


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
    private Transform currentTarget;

    public float clicks = 0;

    private void Awake()
    {
        targetQueue = new Queue<Transform>();
        inQueueCheck = new HashSet<Transform>();
    }

    private void Start()
    {
        target = null;
        clicks = 0;
    }

    private void Update()
    {

        UpdateTarget();
        if (target == null) //If no target does nothing
        {
            return;
        }

        //RotateTower();

        if (attackCountdown <= 0f) //checks if the attack is off cooldown 
        {

            Shoot();

            attackCountdown = 1f / attackSpeed;
        }

        attackCountdown -= Time.deltaTime;



    }


    public virtual void UpdateTarget() //Updates the target to the first target that enters the radius
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

        if (targetQueue.Count > 0)
        {
            if (target == null)
            {
                tempTarget = targetQueue.Dequeue();
                inQueueCheck.Remove(tempTarget);
            }

            if (tempTarget == null)
            {
                return;
            }

            float distanceToRadiusTemp = Vector3.Distance(transform.position, tempTarget.transform.position);

            if (distanceToRadiusTemp > radius)
            {
                //Enqueue(tempTarget);
                target = null;
                tempTarget = null;
                Enqueue(tempTarget);
            }

            else if (distanceToRadiusTemp <= radius)
            {
                target = tempTarget;
            }


        }





    }


    public virtual void RotateTower() //Rotates tower to track target
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(rotatingPart.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }


    public virtual void Shoot()
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

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public virtual void Enqueue(Transform enemy)
    {

        {
            if (inQueueCheck.Add(enemy))
            {
                targetQueue.Enqueue(enemy);
            }
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (clicks == 0)
        {
            Upgrade1();
            clicks++;
        }

        else if (clicks == 1)
        {
            Upgrade2();
            clicks++;
        }
    }

    private void Upgrade1()
    {
        attackDamage = upgrade1Damage;
        radius = upgrade1Range;
        attackSpeed = upgrade1AttackSpeed;

    }


    private void Upgrade2()
    {
        attackDamage = upgrade2Damage;
        radius = upgrade2Range;
        attackSpeed = upgrade2AttackSpeed;

    }
}

