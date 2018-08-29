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
        protected readonly Mover mover;
        protected readonly HandController handController;
        protected readonly EnnemySensor ennemySensor;
        protected Vector3 randomDestination;


        public NormalStrategy(Mover mover, HandController handcontroller, GameObject sight)
        {
            ennemySensor = sight.GetComponent<EnnemySensor>();
            this.mover = mover;
            this.handController = handcontroller;
            FindNewRandomDestination();
        }

        public virtual void Act()
        {
            if (ennemySensor.EnnemiesInSight.Count() != 0)
            {
                Vector3 direction = ennemySensor.EnnemiesInSight.ElementAt(0).transform.position -
                                    mover.transform.position;
                mover.Rotate(Vector2.Dot(direction, mover.transform.right));
                if (Vector3.Distance(mover.transform.position,
                        ennemySensor.EnnemiesInSight.ElementAt(0).transform.position) >= 2)
                {
                    mover.MoveToward(ennemySensor.EnnemiesInSight.ElementAt(0).transform.position);
                }

                handController.Use();
            }
            else
            {
                if (Vector3.Distance(mover.transform.position, randomDestination) <= 0.5)
                {
                    FindNewRandomDestination();
                }


                Vector3 direction = randomDestination - mover.transform.position;
                mover.Rotate(Vector2.Dot(direction, mover.transform.right));
                mover.MoveToward(randomDestination);
            }
        }
        protected void FindNewRandomDestination()
        {
            randomDestination = new Vector3(
                Random.Range(
                    -Camera.main.GetComponent<CameraEdge>().Width / 2,
                    Camera.main.GetComponent<CameraEdge>().Width / 2),
                Random.Range(
                    -Camera.main.GetComponent<CameraEdge>().Height / 2,
                    Camera.main.GetComponent<CameraEdge>().Height / 2),
                0);
        }
    }
}