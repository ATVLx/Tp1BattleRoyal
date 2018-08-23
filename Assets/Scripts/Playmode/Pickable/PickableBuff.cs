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
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Enemy"))
            {
                GetPicked(other);
            }
        }
    }
