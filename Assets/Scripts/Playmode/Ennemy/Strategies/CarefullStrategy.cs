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
    [CreateAssetMenu(fileName = "CarefullStrategy", menuName = "Strategies/Carefull")]
    public class CarefullStrategy : NormalStrategy
    {
        [SerializeField] private int CRITICAL_HEALTH = 25;
        [SerializeField] private int CAREFULL_SAFE_RANGE = 6;
        private Health health;


        public override void Init(Mover mover, HandController handcontroller, GameObject sight)
        {
            base.Init(mover, handcontroller, sight);
            medKitSensor = sight.GetComponent<PickableMedKitSensor>();
            weaponSensor = sight.GetComponent<PickableWeaponSensor>();
            health = mover.GetComponent<Health>();
        }

        protected override void FindSomethingToDo()
        {
            //if strategy see a healthpack and under critical health go there
            if (HasMedKitInSight() && health.HealthPoints <= CRITICAL_HEALTH)
            {
                MoveAndRotateTowardPosition(medKitSensor.MedKitInSight.First().transform.position);
            }
            else if (HasWeaponInSight())
            {
                MoveAndRotateTowardPosition(weaponSensor.WeaponsInSight.First().transform.position);
            }
            //if not under criticalhealth shoot ennemy in sight
            else if (HasTarget())
            {
                Attack();
            }
            //if nothing in sight move randomly
            else
            {
                if (HasReachedDestination())
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

            if (Vector3.Distance(mover.transform.position, targetPos) <= CAREFULL_SAFE_RANGE)
            {
                mover.MoveToward(-targetPos);
            }
            
            handController.Use();
        }
    }
}