using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Pickable;
using UnityEngine;

public class PickableWeapon : Pickable
{
    protected override void GetPicked(Collider2D other)
    {
        Destroy(GetComponent<PickableWeaponStimulus>());
        Destroy(GetComponent<CircleCollider2D>());
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