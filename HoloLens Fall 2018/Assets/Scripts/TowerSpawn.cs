using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class TowerSpawn : MonoBehaviour, IInputHandler, IInputClickHandler
{
    public GameObject spawnPoint;
    public GameObject towerPrefab;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Instantiate(towerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
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
