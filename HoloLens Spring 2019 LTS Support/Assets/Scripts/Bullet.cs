using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Prefab Names")]
    public string[] enemies;

    [Header("Money Gain Values")]
    public int pumpkinKill = 1;

    [Header("Bullet Stats")]
    public float bulletSpeed;
    private Transform target;
    private float bulletDamage;

    private BaseEnemy baseEnemy;

    public void Seek(Transform _target, float damage)//Grabs target from TowerTemplate

    {
        target = _target;
        bulletDamage = damage;
        if (target.name == enemies[0])
        {
            baseEnemy = target.GetComponent<BaseEnemy>();
        }
    }


    // Update is called once per frame
    void Update()
    {


        if (target == null)//Checks if there is a target, if not destorys bullet
        {
            Destroy(gameObject);
            return;

        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) //Hit detection
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); //Moves Bullet

    }

    void HitTarget()
    {

        Destroy(this.gameObject);

        if (target.gameObject.name == enemies[0])
        {
            if (baseEnemy.health > 0)
            {
                baseEnemy.health -= bulletDamage;
            }
            if (baseEnemy.health <= 0)
            {
                Destroy(target.gameObject);
                GameManager.instance.money += pumpkinKill;
            }

        }


        if (target.name == enemies[1])
        {
            Destroy(target.gameObject);
        }


    }

}
