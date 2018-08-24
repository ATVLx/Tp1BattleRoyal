using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Status;
using Playmode.Util.Values;
using UnityEngine;


public class PickableBuff : Pickable
{
    enum type
    {
        MedKit,
        Star
    }

    protected override void GetPicked(Collider2D other)
    {
        //buff the other
       // other.transform.GetComponent<Health>().Hit(-50);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
        {
            GetPicked(other);
        }
    }

}