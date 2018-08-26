using System.Collections;
using System.Collections.Generic;
using Playmode.Ennemy;
using Playmode.Entity.Status;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<EnnemyController> potentialWinners;

    public void AddPotentialWinner(EnnemyController ennemyController)
    {
        potentialWinners.Add(ennemyController);
    }

}
