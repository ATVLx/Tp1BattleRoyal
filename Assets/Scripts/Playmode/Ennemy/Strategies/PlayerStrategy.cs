using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    [CreateAssetMenu(fileName = "PlayerStrategy", menuName = "Strategies/EmptyForPlayer")]
    public class PlayerStrategy:NormalStrategy
    {
        public override void Init(Mover mover, HandController handController, GameObject sight)
        {
            //dont need this
            
        }

        public override void Act()
        {
            //nothing
        }

        public override void DefendModeEngaged(EnnemyController treath)
        {
           //nothing
        }

        protected override void FindSomethingToDo()
        {
            //let the player do his things
        }
    }
}