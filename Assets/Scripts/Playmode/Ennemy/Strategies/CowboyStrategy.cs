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
    public class CowboyStrategy : NormalStrategy
    {
        private Vector3 target;
        private PickableWeaponSensor weaponSensor;

        public CowboyStrategy(Mover mover, HandController handcontroller, GameObject sight) : base(mover,
            handcontroller, sight)
        {
            weaponSensor = sight.GetComponent<PickableWeaponSensor>();
        }

        protected override void FindSomethingToDo()
        {
            //Priorise la recherche d'arme.
            //Si aucune arme , chercher un ennemy.
            if (weaponSensor.WeaponsInSight.Any())
            {
                MoveAndRotateTowardPosition(weaponSensor.WeaponsInSight.First().transform.position);
            }
            else if (ennemySensor.EnnemiesInSight.Count() != 0)
            {
                Attack();
            }
            else if (Vector3.Distance(mover.transform.position, randomDestination) <= 0.5)
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