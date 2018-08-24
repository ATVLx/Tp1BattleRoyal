using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;

namespace Playmode.Ennemy.Strategies
{
    public class NormalStrategy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private readonly EnnemySensor ennemySensor;
        private Vector3 mouvement = new Vector3(100, 100, 0);


        public NormalStrategy(Mover mover, HandController handcontroller, EnnemySensor ennemySensor)
        {
            this.ennemySensor = ennemySensor;
            this.mover = mover;
            this.handController = handcontroller;
        }

        public void Act()
        {
            if (ennemySensor.EnnemiesInSight.Count() != 0)
            {
                //TODO: mover.Rotate();
                mover.Move(ennemySensor.EnnemiesInSight.ElementAt(0).transform.position);
                handController.Use();
            }
            else
            {
                //TODO:random
                mover.Move(mouvement);
            }
        }
    }
}