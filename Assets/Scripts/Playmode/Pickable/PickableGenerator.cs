using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PickableGenerator : MonoBehaviour
{

	[SerializeField] private GameObject[] pickablePrefabs;
	[SerializeField] private int pickableNumber;
	[SerializeField] private int maxSpawnPosY;
	[SerializeField] private int minSpawnPosX;
	[SerializeField] private int minSpawnPosY;
	[SerializeField] private int maxSpawnPosX;
	private UnityEngine.Random rnd;
	
	
	// Use this for initialization
	void Start () {
		rnd=new Random();
		for (int i = 0; i < pickableNumber; i++)
		{
			Instantiate(
				pickablePrefabs[Random.Range(0,pickablePrefabs.Length)],
				new Vector3(Random.Range(minSpawnPosX,maxSpawnPosX),Random.Range(minSpawnPosY,maxSpawnPosY),0),
				Quaternion.identity,
				transform);
		}
	}

}
