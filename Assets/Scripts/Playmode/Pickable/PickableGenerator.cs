using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Playmode.Application;
using Playmode.Pickable;
using UnityEngine;

public class PickableGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] pickablePrefabs;
    [SerializeField] private int pickableNumber;

    // Use this for initialization
    private void Start()
    {
        float maxSpawnPosY = Camera.main.GetComponent<CameraEdge>().Height / 2;
        float minSpawnPosX = -Camera.main.GetComponent<CameraEdge>().Width / 2;
        float minSpawnPosY = -Camera.main.GetComponent<CameraEdge>().Height / 2;
        float maxSpawnPosX = Camera.main.GetComponent<CameraEdge>().Width / 2;

        for (int i = 0; i < pickableNumber; i++)
        {
            GameObject pickable =Instantiate(
                pickablePrefabs[Random.Range(0, pickablePrefabs.Length)],
                new Vector3(Random.Range(minSpawnPosX, maxSpawnPosX), Random.Range(minSpawnPosY, maxSpawnPosY), 0),
                Quaternion.identity,
                transform);
           
            pickable.transform.Rotate(0,0,Random.Range(0,360));
        }
    }

    
}