using System.Linq;
using Playmode.Application;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Movement;
using UnityEditor;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class CarefullStrategy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private readonly EnnemySensor ennemySensor;
        private readonly PickableMedKitSensor medKitSensor;
        private Vector3 randomDestination;
        readonly private Health health;


        private const int CRITICAL_HEALTH = 25;
        [SerializeField] private int CarefullShootingRange = 6;

        public CarefullStrategy(Mover mover, HandController handcontroller, GameObject sight)
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
            //if strategy see a healthpack and under critical health go there
            if (medKitSensor.MedKitInSight.Any() && health.HealthPoints <= CRITICAL_HEALTH)
            {
                Vector3 direction = medKitSensor.MedKitInSight.First().transform.position -
                                    mover.transform.position;
                mover.Rotate(Vector2.Dot(direction, mover.transform.right));
                mover.MoveToward(medKitSensor.MedKitInSight.First().transform.position);
            }
           //if not under criticalhealth shoot ennemy in sight
           else if (ennemySensor.EnnemiesInSight.Count() != 0)
           {
               Vector3 direction = ennemySensor.EnnemiesInSight.ElementAt(0).transform.position -
                                   mover.transform.position;
               mover.Rotate(Vector2.Dot(direction, mover.transform.right));
               if (Vector3.Distance(mover.transform.position,
                       ennemySensor.EnnemiesInSight.ElementAt(0).transform.position) >= CarefullShootingRange)
               {
                   mover.MoveToward(ennemySensor.EnnemiesInSight.ElementAt(0).transform.position);
               }
               else
               {
                   mover.MoveToward(-ennemySensor.EnnemiesInSight.ElementAt(0).transform.position);
               }
               handController.Use();
           }
           //if nothing in sight move randomly
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