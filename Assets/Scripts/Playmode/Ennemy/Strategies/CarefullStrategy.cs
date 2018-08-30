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
    public class CarefullStrategy : NormalStrategy, IEnnemyStrategy
    {
        private readonly PickableMedKitSensor medKitSensor;
        readonly private Health health;
        private const int CRITICAL_HEALTH = 25;
        private int CAREFULL_SAFE_RANGE = 6;


        public CarefullStrategy(Mover mover, HandController handcontroller, GameObject sight) : base(mover,
            handcontroller, sight)
        {
            this.medKitSensor = sight.GetComponent<PickableMedKitSensor>();
            health = mover.GetComponent<Health>();
        }

        public void FindSomethingToDo()
        {
            //if strategy see a healthpack and under critical health go there
            if (medKitSensor.MedKitInSight.Any() && health.HealthPoints <= CRITICAL_HEALTH)
            {
                MoveAndRotateTowardPosition(medKitSensor.MedKitInSight.First().transform.position);
            }
            //if not under criticalhealth shoot ennemy in sight
            else if (ennemySensor.EnnemiesInSight.Count() != 0)
            {
                Attack();
            }
            //if nothing in sight move randomly
            else
            {
                if (Vector3.Distance(mover.transform.position, randomDestination) <= 0.5)
                {
                    FindNewRandomDestination();
                }
                MoveAndRotateTowardPosition(randomDestination);
            }
        }

        protected override void Attack()
        {
            Vector3 targetPos = ennemySensor.EnnemiesInSight.First().transform.position;
            
            RotateTowardPosition(targetPos);
            
            if (Vector3.Distance(mover.transform.position, targetPos) >= CAREFULL_SAFE_RANGE)
            {
                mover.MoveToward(targetPos);
            }
            else
            {
                mover.MoveToward(-targetPos);
            }

            handController.Use();
        }
    }
}