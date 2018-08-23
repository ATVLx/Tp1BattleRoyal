using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void HitSensorEventHandler(int hitPoints); //contrat de l'evenement.

    public class HitSensor : MonoBehaviour
    {
        public event HitSensorEventHandler OnHit; //juste une declaration de l'evenement

        public void Hit(int hitPoints)
        {
            NotifyHit(hitPoints); //notify de l'evenement
        }

        private void NotifyHit(int hitPoints)
        {
            if (OnHit != null) OnHit(hitPoints); //appel l'evenement
        }
    }
}