using System.Linq;
using Playmode.Application;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    [CreateAssetMenu(fileName = "CamperStrategy", menuName = "Strategies/Camper")]
    public class CamperStrategy : NormalStrategy
    {
        [SerializeField] private const float CAMPING_AROUND_MEDKIT_RANGE = 2;
        private Health health;
        private PickableMedKit targetMedKit;


        public override void Init(Mover mover, HandController handcontroller, GameObject sight)
        {
            base.Init(mover, handcontroller, sight);
            this.medKitSensor = sight.GetComponent<PickableMedKitSensor>();
            health = mover.GetComponent<Health>();
        }

        protected override void FindSomethingToDo()
        {
            //BEN_CORRECTION : Remplacez les commentaires par du code lorsque possible.
            //
            //                 Ex : if (HasFoundMedkit)
            
            //if i dont know where any medkit are 
            if (targetMedKit == null)
            {
                //if i see a medkit make it my target
                if (HasMedKitInSight()) //BEN_REVIEW : Ce que vous avez pourtant fait ici.
                {
                    targetMedKit = medKitSensor.MedKitInSight.First();
                    MoveAndRotateTowardPosition(targetMedKit.transform.position);
                }

                else if (HasTarget())
                {
                    Attack();
                }
                //if not move randomly until i find a medkit
                else
                {
                    if (HasReachedDestination())
                    {
                        FindNewRandomDestination();
                    }

                    MoveAndRotateTowardPosition(randomDestination);
                }
            }

            //Si le camper a vu un medkit.
            if (targetMedKit != null)
            {
                //Ramasse uniquement le medkit si ses points de vie sont trop bas.
                if (health.HealthPoints < health.MaxHealth * .5)
                {
                    MoveAndRotateTowardPosition(targetMedKit.transform.position);
                }
                // "Camp"
                else if (Vector2.Distance(mover.transform.position, targetMedKit.transform.position) >
                         CAMPING_AROUND_MEDKIT_RANGE)
                {
                    MoveAndRotateTowardPosition(targetMedKit.transform.position);
                }

                else if (HasTarget())
                {
                    Attack();
                }

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