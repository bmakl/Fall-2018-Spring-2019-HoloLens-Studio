using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideTest : TowerTemplate {

	// Use this for initialization
	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {

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



}
