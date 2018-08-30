using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Application
{
    public delegate void EnnemyDeathEventHandler(Transform ennemyTransformRoot);
    public class EnnemyDeathEventChannel : MonoBehaviour
    {
        public event EnnemyDeathEventHandler OnEnnemyDie;

        public void OnDeath(Transform ennemyTransformRoot)
        {
            NotifyEnnemyDied(ennemyTransformRoot);
        }

        private void NotifyEnnemyDied(Transform ennemyTransformRoot)
        {
            if (OnEnnemyDie!= null) OnEnnemyDie(ennemyTransformRoot);
        }
    }
}