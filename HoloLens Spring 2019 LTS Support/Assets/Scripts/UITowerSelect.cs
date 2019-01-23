using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class UITowerSelect : MonoBehaviour, IInputHandler, IInputClickHandler
{
    public GameObject tower;
    public GameObject[] nodes;

    void Start()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("nice click");
        foreach (GameObject node in nodes)
        {
            Debug.Log("node lol");
            node.GetComponent<TowerSpawn>().turretToBuild = tower;
        }
    }

    public void OnInputDown(InputEventData eventData)
    {
        Debug.Log("down click");
    }

    public void OnInputUp(InputEventData eventData)
    {
        Debug.Log("up click");
    }
}
