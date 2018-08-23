using Playmode.Ennemy.BodyParts;
using UnityEngine;

public class PickableWeapon : Pickable
{

    protected override void GetPicked(Collision2D other)
    {
        //add to hand
        other.transform.GetComponent<HandController>().Hold(this.gameObject);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            GetPicked(other);
        }
    }
}