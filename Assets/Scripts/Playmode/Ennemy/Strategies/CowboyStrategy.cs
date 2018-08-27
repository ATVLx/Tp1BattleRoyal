using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Playmode.Application;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;


namespace Playmode.Ennemy.Strategies
{
  public class CowboyStrategy : IEnnemyStrategy
  {
    private readonly Mover mover;
    private readonly HandController handController;
    private readonly EnnemySensor ennemySensor;
    private Vector3 randomDestination;


    public CowboyStrategy(Mover mover, HandController handcontroller, EnnemySensor ennemySensor)
    {
      this.ennemySensor = ennemySensor;
      this.mover = mover;
      this.handController = handcontroller;
      randomDestination = new Vector3(
        Random.Range(-Camera.main.GetComponent<CameraEdge>().Width / 2, Camera.main.GetComponent<CameraEdge>().Width / 2),
        Random.Range(-Camera.main.GetComponent<CameraEdge>().Height / 2,Camera.main.GetComponent<CameraEdge>().Height / 2),
        0);
    }

    public void Act()
    {
      //Priorise la recherche d'arme. Creer un cerle autour du cowboy pour detection des armes. Ajouter les armes dans une liste a prendre.
      //Si aucune arme , chercher un ennemy.
      //if()
     // {

      //}



    // if (ennemySensor.EnnemiesInSight.Count() != 0)
    //{
    //  Vector3 direction = ennemySensor.EnnemiesInSight.ElementAt(0).transform.position - mover.transform.position;
    //  mover.Rotate(Vector2.Dot(direction, mover.transform.right));
    //  if (Vector3.Distance(mover.transform.position, ennemySensor.EnnemiesInSight.ElementAt(0).transform.position) >= 2)
    //  {
    //    mover.MoveToward(ennemySensor.EnnemiesInSight.ElementAt(0).transform.position);
    //  }
    //  handController.Use();
    //}
    }

  }

}

