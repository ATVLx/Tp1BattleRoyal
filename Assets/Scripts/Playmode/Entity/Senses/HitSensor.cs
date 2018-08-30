using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void HitSensorEventHandler(float hitPoints , Transform source); //contrat de l'evenement.

    public class HitSensor : MonoBehaviour
    {
        public event HitSensorEventHandler OnHit; //juste une declaration de l'evenement

        public void Hit(float hitPoints , Transform source)
        {
            NotifyHit(hitPoints , source); //notify de l'evenement
        }

        private void NotifyHit(float hitPoints ,Transform source)
        {
            if (OnHit != null) OnHit(hitPoints , source); //appel l'evenement
        }
    }
}