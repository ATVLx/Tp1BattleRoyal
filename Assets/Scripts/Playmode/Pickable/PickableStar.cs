using Playmode.Entity.Status;
using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableStar :Pickable
    {
        [SerializeField] private int durationInSeconds;
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            GetPicked(other);
        }

        protected override void GetPicked(Collider2D other)
        {
            other.GetComponent<Health>().Invincibility(durationInSeconds);
        }
    }
}