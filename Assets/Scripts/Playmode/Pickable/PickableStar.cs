using Playmode.Ennemy;
using Playmode.Entity.Status;
using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableStar : Pickable
    {
        [SerializeField] private int durationInSeconds;


        protected override bool GetPicked(Transform other)
        {
            other.transform.root.GetComponentInChildren<Health>().Invincibility(durationInSeconds);
            Destroy(this.gameObject);
            return true;
        }
    }
}