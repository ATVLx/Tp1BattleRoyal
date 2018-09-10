﻿using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Application
{
    public delegate void EnnemyDeathEventHandler(EnnemyController ennemyController);

    public class EnnemyDeathEventChannel : MonoBehaviour
    {
        public event EnnemyDeathEventHandler OnEnnemyDie;

        //BEN_REVIEW : La convention de nommage pour les EventChannel, c'est "PublishSomething".
        //             Dans votre cas, ça donnerait "PublishEnemyDeath".
        public void OnDeath(EnnemyController ennemyController)
        {
            NotifyEnnemyDied(ennemyController);
        }

        private void NotifyEnnemyDied(EnnemyController ennemyController)
        {
            if (OnEnnemyDie != null) OnEnnemyDie(ennemyController);
        }
    }
}