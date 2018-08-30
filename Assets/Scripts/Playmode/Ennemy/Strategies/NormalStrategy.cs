using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Playmode.Application;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;

namespace Playmode.Ennemy.Strategies
{
    public class NormalStrategy : IEnnemyStrategy
    {
        private const int SPACE_BETWEEN_ENNEMIES = 4;
        
        protected readonly Mover mover;
        protected readonly HandController handController;
        protected readonly EnnemySensor ennemySensor;
        
        protected Vector3 randomDestination;
        protected EnnemyController treath;


        public NormalStrategy(Mover mover, HandController handcontroller, GameObject sight)
        {
            ennemySensor = sight.GetComponent<EnnemySensor>();
            this.mover = mover;
            this.handController = handcontroller;
            
            FindNewRandomDestination();
        }

        public void Act()
        {
            if (!HasTarget() && IsThreaten())
            {
                Defend();
            }
            else
            {
                FindSomethingToDo();
            }
            
        }

        private bool IsThreaten()
        {
            return treath != null;
        }

        private bool HasTarget()
        {
            return ennemySensor.EnnemiesInSight.Count() > 0;
        }

        protected void FindNewRandomDestination()
        {
            randomDestination = new Vector3(
                Random.Range(
                    -Camera.main.GetComponent<CameraEdge>().Width / 2, //aller chercher camera une seule fois
                    Camera.main.GetComponent<CameraEdge>().Width / 2),
                Random.Range(
                    -Camera.main.GetComponent<CameraEdge>().Height / 2,
                    Camera.main.GetComponent<CameraEdge>().Height / 2),
                0);
        }
        protected void MoveAndRotateTowardPosition(Vector3 position)
        {
            RotateTowardPosition(position);
            mover.MoveToward(position);
        }

        protected void RotateTowardPosition(Vector3 position)
        {
            Vector3 direction = position -mover.transform.position;
            mover.Rotate(Vector2.Dot(direction, mover.transform.right));
        }

        public void DefendModeEngaged(EnnemyController treath)
        {
            this.treath = treath;
        }

        protected virtual void FindSomethingToDo()
        {
            if (HasTarget())
            {
               Attack();
            }
            else
            {
                if (HasReachedDestination())
                {
                    FindNewRandomDestination();
                }

                MoveAndRotateTowardPosition(randomDestination);
            }
        }

        private bool HasReachedDestination()
        {
            return Vector3.Distance(mover.transform.position, randomDestination) <= 0.5;
        }

        protected virtual void Attack()
        {
            RotateTowardPosition(ennemySensor.EnnemiesInSight.First().transform.position);  
            if (Vector3.Distance(mover.transform.position,
                    ennemySensor.EnnemiesInSight.ElementAt(0).transform.position) >= SPACE_BETWEEN_ENNEMIES)
            {
                mover.MoveToward(ennemySensor.EnnemiesInSight.ElementAt(0).transform.position);
            }
            handController.Use();
        }

        protected void Defend()
        {
            Vector3 direction = treath.transform.position - mover.transform.position;
            mover.Rotate(Vector2.Dot(direction,mover.transform.right));
        }
    
        
    }
}