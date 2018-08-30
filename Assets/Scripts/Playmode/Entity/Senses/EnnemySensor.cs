using System.Collections.Generic;
using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void EnnemySensorEventHandler(EnnemyStimulus ennemy);

    public class EnnemySensor : MonoBehaviour
    {
        private HashSet<EnnemyStimulus> ennemiesInSight;

        public event EnnemySensorEventHandler OnEnnemySeen;
        public event EnnemySensorEventHandler OnEnnemySightLost;


        public IEnumerable<EnnemyStimulus> EnnemiesInSight {
          get {
            ennemiesInSight.RemoveWhere(it => it == null);
            return ennemiesInSight;
          }
        }


        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            ennemiesInSight = new HashSet<EnnemyStimulus>();
        }

        public void See(EnnemyStimulus ennemy)
        {
            ennemiesInSight.Add(ennemy);

            NotifyEnnemySeen(ennemy);
        }


        public void LooseSightOf(EnnemyStimulus ennemy)
        {
            ennemiesInSight.Remove(ennemy);

            NotifyEnnemySightLost(ennemy);
        }

        private void NotifyEnnemySeen(EnnemyStimulus ennemy)
        {
            if (OnEnnemySeen != null) OnEnnemySeen(ennemy);
        }

        private void NotifyEnnemySightLost(EnnemyStimulus ennemy)
        {
            if (OnEnnemySightLost != null) OnEnnemySightLost(ennemy);
        }

    }
}