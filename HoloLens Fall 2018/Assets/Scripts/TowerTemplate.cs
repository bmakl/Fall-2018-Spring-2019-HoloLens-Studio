using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTemplate : MonoBehaviour {


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


     private void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    private void Update()
    {
        if (target == null) //If no target does nothing
        {
            return;
        }

        RotateTower();

        if (attackCountdown <= 0f) //checks if the attack is off cooldown 
        {
            Shoot();
            attackCountdown = 1f / attackSpeed;
        }

        attackCountdown -= Time.deltaTime;

    }


    public virtual void UpdateTarget () //Updates the target to the first target that enters the radius
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity; //infinate distance to nearest enemy by default
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            } 
        }

        if (nearestEnemy != null && shortestDistance <= radius) //if enemy leaves radius sets new enemy
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
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
            bullet.Seek(target); //passes the target to bullet script
        } else if (bullet == null)
        {
            Debug.LogError("Bullet does not exist");
        }
    }

    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }



}

