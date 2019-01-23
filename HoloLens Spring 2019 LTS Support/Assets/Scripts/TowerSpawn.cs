using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class TowerSpawn : MonoBehaviour, IInputHandler, IInputClickHandler
{
    public GameObject spawnPoint;
    //[HideInInspector]
    public GameObject turretToBuild;
    public GameObject towerPrefab;
    public bool nodeEnabled = true;

    Renderer rend;
    public void Start()
    {
        rend = GetComponent<MeshRenderer>();

        if (!nodeEnabled)
        {
            rend.material.color = Color.red;
        }

    }


    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (nodeEnabled)
        {
            if (towerPrefab != null)
            {
                Debug.Log("Already Turret On Node");
                return;
            }
            towerPrefab = turretToBuild;
            Instantiate(towerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else
        {

        }
    }

    /* this was to test if the turret actually spawns but my PC (Thomas Murphy) won't let me click with the Hololens Kit.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(towerPrefab, transform.position, transform.rotation);
        }
    }
    */

    public void OnInputDown(InputEventData eventData)
    {
        Debug.Log("down click");
    }

    public void OnInputUp(InputEventData eventData)
    {
        Debug.Log("up click");
    }
}
