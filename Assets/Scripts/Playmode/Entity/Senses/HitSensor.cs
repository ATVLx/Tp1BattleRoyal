using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void HitSensorEventHandler(int hitPoints , EnnemyController source); //contrat de l'evenement.

    public class HitSensor : MonoBehaviour
    {
        public event HitSensorEventHandler OnHit; //juste une declaration de l'evenement

        public void Hit(int hitPoints , EnnemyController source)
        {
            NotifyHit(hitPoints , source); //notify de l'evenement
        }

        private void NotifyHit(int hitPoints ,EnnemyController source)
        {
            if (OnHit != null) OnHit(hitPoints , source); //appel l'evenement
        }
    }
}