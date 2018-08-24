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
  public class NormalStrategy : IEnnemyStrategy
  {
    private readonly Mover mover;
    private readonly HandController handController;
    private readonly EnnemySensor ennemySensor;
    private Vector3 randomDestination;
    float mapEdgeY = Camera.main.GetComponent<CameraEdge>().Height / 2;
    float mapEdgeX=  Camera.main.GetComponent<CameraEdge>().Width / 2;

    public NormalStrategy(Mover mover, HandController handcontroller, EnnemySensor ennemySensor)
    {
      this.ennemySensor = ennemySensor;
      this.mover = mover;
      this.handController = handcontroller;
      randomDestination = new Vector3(Random.Range(-mapEdgeX,mapEdgeX),Random.Range(-mapEdgeY,mapEdgeY), 0);

    }

    public void Act()
    {
     if (ennemySensor.EnnemiesInSight.Count() != 0)
     {
        Vector3 direction = ennemySensor.EnnemiesInSight.ElementAt(0).transform.position - mover.transform.position;
        mover.Rotate(Vector2.Dot(direction, mover.transform.right));
        if (Vector3.Distance(mover.transform.position, ennemySensor.EnnemiesInSight.ElementAt(0).transform.position)>=2)
        {
          mover.MoveToward(ennemySensor.EnnemiesInSight.ElementAt(0).transform.position);
        }
        handController.Use();
     }
     else
     {
     
      if (Vector3.Distance(mover.transform.position, randomDestination) <= 0.5)
      {
        
        randomDestination = new Vector3(
          Random.Range(-mapEdgeX,mapEdgeX),Random.Range(-mapEdgeY,mapEdgeY),
          0);
      }

        
        Vector3 direction = randomDestination - mover.transform.position;
        mover.Rotate(Vector2.Dot(direction , mover.transform.right));
        mover.MoveToward(randomDestination);
      }
   }
  }
}