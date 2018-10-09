using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;

    public float bulletSpeed;


    public void Seek(Transform _target)//Grabs target from TowerTemplate

    {
        target = _target;
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
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); //Moves Bullet

	}

    void HitTarget()
    {
        Destroy(this.gameObject);
        Destroy(target.gameObject);
    }

}
