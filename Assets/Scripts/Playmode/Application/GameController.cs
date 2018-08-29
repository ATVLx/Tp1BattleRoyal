using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Playmode.Ennemy;
using Playmode.Entity.Status;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<EnnemyController> potentialWinners;

    private void Awake()
    {
        potentialWinners=new List<EnnemyController>();
    }

    public void AddPotentialWinner(EnnemyController ennemyController)
    {
        potentialWinners.Add(ennemyController);
        ennemyController.GetComponent<Health>().OnDeath += OnPotentialWinnerDeath;
    }

    private void OnPotentialWinnerDeath(EnnemyController ennemyController)
    {
        potentialWinners.Remove(ennemyController);
        Camera.main.GetComponent<CameraController>().Shrink();	
        if (potentialWinners.Count == 1)
        {
            Camera.main.GetComponent<CameraController>().StartFollowing(potentialWinners.ElementAt(0).transform);
        }
    }

}
