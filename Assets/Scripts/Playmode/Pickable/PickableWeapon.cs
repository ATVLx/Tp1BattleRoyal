using Playmode.Ennemy;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Pickable;
using UnityEngine;

public class PickableWeapon : Pickable
{
    protected override bool GetPicked(EnnemyController other)
    {
        Destroy(GetComponent<PickableWeaponStimulus>());
        Destroy(GetComponent<CircleCollider2D>());
        //Ajout à la main.
        other.transform.root.GetComponentInChildren<HandController>().Hold(this.gameObject);
        Destroy(this);
        return true;
    }
}