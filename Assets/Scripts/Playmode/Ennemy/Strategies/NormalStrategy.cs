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
    [CreateAssetMenu(fileName = "NormalStrategy", menuName = "Strategies/Normal")]
    public class NormalStrategy : EnnemyStrategy
    {
        public override void Init(Mover mover, HandController handController, GameObject sight)
        {
            ennemySensor = sight.GetComponent<EnnemySensor>();
            this.mover = mover;
            this.handController = handController;
            cameraEdge = Camera.main.GetComponent<CameraEdge>();
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraEventChannel>().OnCameraChange +=
                FindNewRandomDestination;
            FindNewRandomDestination();
        }


        public virtual void DefendModeEngaged(EnnemyController treath)
        {
            this.treath = treath;
        }

        protected override void FindSomethingToDo()
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
    }
}