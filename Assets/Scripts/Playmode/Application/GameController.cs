using System.Collections;
using System.Collections.Generic;
using Playmode.Ennemy;
using Playmode.Entity.Status;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private HashSet<EnnemyController> potentialWinners;

    private void Awake()
    {
        potentialWinners=new HashSet<EnnemyController>();
    }

    public void AddPotentialWinner(EnnemyController ennemyController)
    {
        potentialWinners.Add(ennemyController);
        ennemyController.GetComponent<Health>().OnDeath += OnPotentialWinnerDeath;
    }

    private void OnPotentialWinnerDeath()
    {
       potentialWinners.RemoveWhere(it => it == null);
        Camera.main.GetComponent<CameraController>().Shrink(potentialWinners.Count);	
        if (potentialWinners.Count == 1)
        {
            //win
        }
    }

}
