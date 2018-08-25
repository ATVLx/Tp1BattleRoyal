using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Status;
using Playmode.Util.Values;
using UnityEngine;


public class PickableMedKit : Pickable
{
    [SerializeField] private int healthPoint;
    protected override void GetPicked(Collider2D other)
    {
       other.transform.GetComponent<Health>().Heal(healthPoint);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
        {
            GetPicked(other);
        }
    }

}