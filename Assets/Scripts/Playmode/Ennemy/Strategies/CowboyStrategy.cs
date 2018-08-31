using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Playmode.Application;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;
using Playmode.Pickable;
using Random = UnityEngine.Random;


namespace Playmode.Ennemy.Strategies
{
    [CreateAssetMenu(fileName = "CowboyStrategy", menuName = "Strategies/Cowboy")]
    public class CowboyStrategy : NormalStrategy
    {
        private Vector3 target;

        public override void Init(Mover mover, HandController handcontroller, GameObject sight)
        {
            base.Init(mover, handcontroller, sight);
            weaponSensor = sight.GetComponent<PickableWeaponSensor>();
        }

        protected override void FindSomethingToDo()
        {
            //Priorise la recherche d'arme.
            //Si aucune arme , chercher un ennemy.
            if (HasWeaponInSight())
            {
                MoveAndRotateTowardPosition(weaponSensor.WeaponsInSight.First().transform.position);
            }
            else if (HasTarget())
            {
                Attack();
            }
            else if (HasReachedDestination())
            {
                FindNewRandomDestination();
            }
            else
            {
                MoveAndRotateTowardPosition(randomDestination);
            }
        }
    }
}