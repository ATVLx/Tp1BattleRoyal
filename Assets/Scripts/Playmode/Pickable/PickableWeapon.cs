using Playmode.Ennemy.BodyParts;
using UnityEngine;

public class PickableWeapon : Pickable
{
    protected override void GetPicked(Collider2D other)
    {
        //add to hand
        other.transform.root.GetComponentInChildren<HandController>().Hold(this.gameObject);
        Destroy(this);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
        {
            GetPicked(other);
        }
    }

}