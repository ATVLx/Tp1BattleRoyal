using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Status;
using UnityEngine;


    public class PickableBuff : Pickable
    {
        enum type
        {
            MedKit,
            Star
        }
        protected override void GetPicked(Collision2D other)
        {
            //buff the other
            other.transform.GetComponent<Health>().Hit(-50);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Enemy"))
            {
                GetPicked(other);
            }
        }
    }
