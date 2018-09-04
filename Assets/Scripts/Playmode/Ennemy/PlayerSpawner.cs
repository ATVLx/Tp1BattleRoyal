using System.Collections;
using System.Collections.Generic;
using Playmode.Application;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enterGameText;

    [SerializeField] private GameObject playerPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraEdge edge = Camera.main.GetComponent<CameraEdge>();
            Destroy(enterGameText);
            Vector2 spawnPos = new Vector2(
                Random.Range(-edge.Width / 2, edge.Width / 2),
                Random.Range(-edge.Height / 2, edge.Height / 2));
            Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}