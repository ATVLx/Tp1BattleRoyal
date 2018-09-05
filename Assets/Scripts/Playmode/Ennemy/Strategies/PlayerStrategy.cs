using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    [CreateAssetMenu(fileName = "PlayerStrategy", menuName = "Strategies/EmptyForPlayer")]
    public class PlayerStrategy : NormalStrategy
    {
        public override void Init(Mover mover, HandController handController, GameObject sight)
        {
            
        }

        public override void Act()
        {
            
        }

        public override void SetThreat(EnnemyController treath)
        {
            
        }

        protected override void FindSomethingToDo()
        {
            
        }
    }
}