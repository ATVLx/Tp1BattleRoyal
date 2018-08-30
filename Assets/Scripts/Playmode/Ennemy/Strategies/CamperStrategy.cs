using System.Linq;
using Playmode.Application;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class CamperStrategy : NormalStrategy, IEnnemyStrategy
    {
        private readonly PickableMedKitSensor medKitSensor;
        readonly private Health health;
        private PickableMedKit targetMedKit;
        private const float CAMPING_AROUND_MEDKIT_RANGE = 2;


        public CamperStrategy(Mover mover, HandController handcontroller, GameObject sight) : base(mover,
            handcontroller, sight)
        {
            this.medKitSensor = sight.GetComponent<PickableMedKitSensor>();
            health = mover.GetComponent<Health>();
        }

        protected override void FindSomethingToDo()
        {
            //if i dont know where any medkit are 
            if (targetMedKit == null)
            {
                //if i see a medkit make it my target
                if (medKitSensor.MedKitInSight.Count() != 0)
                {
                    targetMedKit = medKitSensor.MedKitInSight.First();
                    MoveAndRotateTowardPosition(targetMedKit.transform.position);
                }
                //if not move randomly until i find a medkit
                else
                {
                    if (Vector3.Distance(mover.transform.position, randomDestination) <= 0.5)
                    {
                        FindNewRandomDestination();
                    }

                    MoveAndRotateTowardPosition(randomDestination);
                }
            }

            //if i have a target medkit
            if (targetMedKit != null)
            {
                //if haelth is under 50% go to medkit
                if (health.HealthPoints < health.MaxHealth * .5)
                {
                    MoveAndRotateTowardPosition(targetMedKit.transform.position);
                }
                //else stay close to medkit
                else if (Vector2.Distance(mover.transform.position, targetMedKit.transform.position) > CAMPING_AROUND_MEDKIT_RANGE)
                {
                    MoveAndRotateTowardPosition(targetMedKit.transform.position);
                }
                //im im over medkit and see ennnemy i defend it
                else if (ennemySensor.EnnemiesInSight.Count() != 0)
                {
                    Attack();
                }
                //else i try to find other ennemy around medkit
                else
                {
                    mover.Rotate(1);
                }
            }
        }

        protected override void Attack()
        {
            Vector3 direction = ennemySensor.EnnemiesInSight.First().transform.position -
                                mover.transform.position;
            mover.Rotate(Vector2.Dot(direction, mover.transform.right));
            handController.Use();
        }
    }
}