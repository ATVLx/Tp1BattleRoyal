using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;

namespace Playmode.Ennemy.Strategies
{
  public class NormalStrategy : IEnnemyStrategy
  {
    private readonly Mover mover;
    private readonly HandController handController;
    private readonly EnnemySensor ennemySensor;
    private Vector3 randomDestination;


    public NormalStrategy(Mover mover, HandController handcontroller, EnnemySensor ennemySensor)
    {
      this.ennemySensor = ennemySensor;
      this.mover = mover;
      this.handController = handcontroller;
      randomDestination = new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0);

    }

    public void Act()
    {
     if (ennemySensor.EnnemiesInSight.Count() != 0)
     {
        Vector3 direction = ennemySensor.EnnemiesInSight.ElementAt(0).transform.position - mover.transform.position;
        mover.Rotate(Vector2.Dot(direction, mover.transform.right));
        mover.Move(randomDestination);
        handController.Use();
     }
     else
     {
     
      if (Vector3.Distance(mover.transform.position, randomDestination) <= 0.5)
      {
        randomDestination = new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), 0);
      }

        Vector3 direction = randomDestination - mover.transform.position;
        mover.Move(randomDestination);
        mover.Rotate(Vector2.Dot(direction, mover.transform.right));
      }
   }
  }
}