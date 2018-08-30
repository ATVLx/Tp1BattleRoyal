using System.Linq;
using Boo.Lang;
using Playmode.Application;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public abstract class EnnemyStrategy : ScriptableObject
    {
        [SerializeField]public Sprite sprite;
        [SerializeField]protected int SPACE_BETWEEN_ENNEMIES = 4;
        [SerializeField]protected float DESTINATION_REACHED_PRECISION = 0.5f;
        protected Mover mover;
        protected HandController handController;
        protected EnnemySensor ennemySensor;
        protected CameraEdge cameraEdge;
        protected Vector3 randomDestination;
        protected EnnemyController treath;
        public abstract void Init(Mover mover, HandController handController, GameObject sight);
        public virtual void Act()
        {
            if (IsThreaten()&&!HasTarget() && ThreathIsInRange())
            {
               Defend();
            }
            else
            {
                FindSomethingToDo();
            }
        }

        protected bool IsThreaten()
        {
            return treath != null;
        }

        protected bool ThreathIsInRange()
        {
            return Vector3.Distance(mover.transform.position, treath.transform.position) <=
                   ennemySensor.GetComponentInChildren<PolygonCollider2D>().bounds.size.y;
        }
        
        protected bool HasTarget()
        {
            return ennemySensor.EnnemiesInSight.Count() > 0;
        }

        protected abstract void FindSomethingToDo();

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

        protected  void Defend()
        {
            Vector3 direction = treath.transform.position - mover.transform.position;
            mover.Rotate(Vector2.Dot(direction, mover.transform.right));
            
        }
        
        protected bool HasReachedDestination()
        {
            return Vector3.Distance(mover.transform.position, randomDestination) <= DESTINATION_REACHED_PRECISION;
            
        }
        
        protected void FindNewRandomDestination()
        {
            randomDestination = new Vector3(
                Random.Range(
                    -cameraEdge.Width / 2, //aller chercher camera une seule fois
                    cameraEdge.Width / 2),
                Random.Range(
                    -cameraEdge.Height / 2,
                    cameraEdge.Height / 2),
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

    }
}