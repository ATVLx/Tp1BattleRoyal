using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesSpawnedEventChannel : MonoBehaviour {

	public delegate void EnnemiesSpawnedEventHandler();
		public event EnnemiesSpawnedEventHandler OnAllEnnemiesSpawned;

		public void OnSpawnFinish()
		{
			NotifyAllEnnemiesSpawned();
		}

		private void NotifyAllEnnemiesSpawned()
		{
			if (OnAllEnnemiesSpawned!= null) OnAllEnnemiesSpawned();
		}
}
