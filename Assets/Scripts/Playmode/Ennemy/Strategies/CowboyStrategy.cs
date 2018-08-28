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
    public class CowboyStrategy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private readonly EnnemySensor ennemySensor;
        private Vector3 randomDestination;
        private Vector3 target;
        private PickableWeaponSensor weaponSensor;
        float mapEdgeY = Camera.main.GetComponent<CameraEdge>().Height / 2;
        float mapEdgeX = Camera.main.GetComponent<CameraEdge>().Width / 2;

        public CowboyStrategy(Mover mover, HandController handcontroller, GameObject sightTransform)
        {
            this.ennemySensor = sightTransform.GetComponent<EnnemySensor>();
            this.mover = mover;
            this.weaponSensor = sightTransform.GetComponent<PickableWeaponSensor>();
            this.handController = handcontroller;
            randomDestination = new Vector3(Random.Range(-mapEdgeX, mapEdgeX), Random.Range(-mapEdgeY, mapEdgeY), 0);
        }

        public void Act()
        {
            Debug.Log(weaponSensor.WeaponsInSight.Count());
            //Priorise la recherche d'arme.
            //Si aucune arme , chercher un ennemy.
            try
            {
                if (weaponSensor.WeaponsInSight.Any())
                {
                    Vector3 direction = weaponSensor.WeaponsInSight.First().transform.position -
                                        mover.transform.position;
                    mover.Rotate(Vector2.Dot(direction, mover.transform.right));
                    mover.MoveToward(weaponSensor.WeaponsInSight.First().transform.position);
                }
                else if (ennemySensor.EnnemiesInSight.Count() != 0)
                {
                    Vector3 direction = ennemySensor.EnnemiesInSight.ElementAt(0).transform.position -
                                        mover.transform.position;
                    if (Vector3.Distance(mover.transform.position,
                            ennemySensor.EnnemiesInSight.ElementAt(0).transform.position) >= 2)
                    {
                        mover.MoveToward(ennemySensor.EnnemiesInSight.ElementAt(0).transform.position);
                    }

                    mover.Rotate(Vector2.Dot(direction, mover.transform.right));
                    handController.Use();
                }
                else if (Vector3.Distance(mover.transform.position, randomDestination) <= 0.5)
                {
                    randomDestination = new Vector3(
                        Random.Range(-mapEdgeX, mapEdgeX), Random.Range(-mapEdgeY, mapEdgeY),
                        0);
                }
                else
                {
                    Vector3 direction = randomDestination - mover.transform.position;
                    mover.MoveToward(randomDestination);
                    mover.Rotate(Vector2.Dot(direction, mover.transform.right));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}