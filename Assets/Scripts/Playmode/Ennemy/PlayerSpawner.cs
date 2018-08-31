using System.Collections;
using System.Collections.Generic;
using Playmode.Application;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{

	[SerializeField] private GameObject enterGameText;

	[SerializeField] private GameObject playerPrefab;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Destroy(enterGameText);
			Vector2 spawnPos = new Vector2(
				Random.Range(-Camera.main.GetComponent<CameraEdge>().Width/2,Camera.main.GetComponent<CameraEdge>().Width/2),
				Random.Range(-Camera.main.GetComponent<CameraEdge>().Height/2,Camera.main.GetComponent<CameraEdge>().Height/2));
			Instantiate(playerPrefab, spawnPos, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
