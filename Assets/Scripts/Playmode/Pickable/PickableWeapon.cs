using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Pickable;
using UnityEngine;

public class PickableWeapon : Pickable
{
    protected override void GetPicked(Collider2D other)
    {
        //add to hand
        other.transform.root.GetComponentInChildren<HandController>().Hold(this.gameObject);
        //other.transform.root.GetComponentInChildren<PickableWeaponSensor>().LooseSightOf(this);
        Destroy(this);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //todo:remplace by stimulu and sensor with notify
        if (other.CompareTag("Ennemy"))
        {
            GetPicked(other);
        }
    }

}