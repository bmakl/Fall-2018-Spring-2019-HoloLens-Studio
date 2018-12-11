using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour {

 

    [Header("Money Gain Values")]
    public int pumpkinKill = 1;

    [Header("Bullet Stats")]
    public float bulletSpeed;
    private Transform target;
    public float bulletDamage;

    public BaseEnemy baseEnemy;

    public void Seek(Transform _target, float damage)//Grabs target from TowerTemplate

    {
        target = _target;
        bulletDamage = damage;
      
            baseEnemy = target.GetComponent<BaseEnemy>();
    }


    // Update is called once per frame
    void Update() {


        if (target == null)//Checks if there is a target, if not destorys bullet
        {
            Destroy(gameObject);
            return;

        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) //Hit detection
        {

            if (this != null)
            {
                baseEnemy.HitDetect(this);
                Destroy(this.gameObject);
                return;
            }

        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); //Moves Bullet

	}

 

}
