using System.Linq;
using Playmode.Application;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class CamperStrategy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private readonly EnnemySensor ennemySensor;
        private readonly PickableMedKitSensor medKitSensor;
        private Vector3 randomDestination;
        readonly private Health health;
        private PickableMedKit targetMedKit;


        public CamperStrategy(Mover mover, HandController handcontroller, GameObject sight)
        {
            this.ennemySensor = sight.GetComponent<EnnemySensor>();
            this.medKitSensor = sight.GetComponent<PickableMedKitSensor>();
            this.mover = mover;
            health = mover.GetComponent<Health>();
            this.handController = handcontroller;
            FindNewRandomDestination();
        }

        public void Act()
        {
            //if i dont know where any medkit are 
            if (targetMedKit == null)
            {
                //if i see a medkit make it my target
                if (medKitSensor.MedKitInSight.Count() != 0)
                {
                    targetMedKit = medKitSensor.MedKitInSight.ElementAt(0);
                    Vector3 direction = targetMedKit.transform.position -
                                        mover.transform.position;
                    mover.Rotate(Vector2.Dot(direction, mover.transform.right));
                    mover.MoveToward(targetMedKit.transform.position);
                }
                //if not move randomly until i find a medkit
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
            //if i have a target medkit
            if (targetMedKit != null)
            {
                //if im not over medKit move toward it
                if (Vector2.Distance(mover.transform.position, targetMedKit.transform.position) > 0.1)
                {
                    Vector3 direction = targetMedKit.transform.position - mover.transform.position;
                    mover.Rotate(Vector2.Dot(direction, mover.transform.right));
                    mover.MoveToward(targetMedKit.transform.position);
                }
                //im im over medkit and see ennnemy i defend it
                else if (ennemySensor.EnnemiesInSight.Count() != 0)
                {
                    Vector3 direction = ennemySensor.EnnemiesInSight.ElementAt(0).transform.position -
                                        mover.transform.position;
                    mover.Rotate(Vector2.Dot(direction, mover.transform.right));
                    handController.Use();
                }
                //else i try to find other ennemy around medkit
                else
                {
                    mover.Rotate(1);
                }
            }
        }

        private void FindNewRandomDestination()
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