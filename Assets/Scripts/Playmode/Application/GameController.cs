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
    public List<Transform> potentialWinners;

    public List<Transform> PotentialWinners => potentialWinners;

    [SerializeField] private RectTransform winnerText;

    private void Awake()
    {
        Debug.Log("GC awake");
        potentialWinners=new List<Transform>(); 
        this.GetComponent<EnnemyDeathEventChannel>().OnEnnemyDie+= OnPotentialWinnerDeath;
    }

    public void AddPotentialWinner(Transform ennemyTransform)
    {
        potentialWinners.Add(ennemyTransform);
    }

    private void OnPotentialWinnerDeath(Transform ennemyController)
    {
        potentialWinners.Remove(ennemyController);
        Camera.main.GetComponent<CameraController>().Shrink();	
        
        if (potentialWinners.Count == 1)
        {
            Camera.main.GetComponent<CameraController>().StartFollowing(potentialWinners.First().transform);
            winnerText.GetComponent<Text>().text= potentialWinners.First().root.name + " Won!";
        }
    }

    

}
