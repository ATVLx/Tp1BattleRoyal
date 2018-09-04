using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Playmode.Application;
using Playmode.Ennemy;
using Playmode.Entity.Status;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<EnnemyController> potentialWinners;

    public List<EnnemyController> PotentialWinners => potentialWinners;

    [SerializeField] private RectTransform winnerText;

    private void Awake()
    {
        potentialWinners = new List<EnnemyController>();
        this.GetComponent<EnnemyDeathEventChannel>().OnEnnemyDie += OnPotentialWinnerDeath;
    }

    public void AddPotentialWinner(EnnemyController ennemyController)
    {
        potentialWinners.Add(ennemyController);
    }

    private void OnPotentialWinnerDeath(EnnemyController ennemyController)
    {
        potentialWinners.Remove(ennemyController);
        Camera.main.GetComponent<CameraController>().Shrink();

        if (potentialWinners.Count == 1)
        {
            Camera.main.GetComponent<CameraController>().StartFollowing(potentialWinners.ElementAt(0).transform);
            winnerText.GetComponent<Text>().text = potentialWinners.First().transform.root.name + " Won!";
        }
    }
}