using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Util.Values;
using UnityEngine;


public class PickableMedKit : Pickable
{
    [SerializeField] private int healthPoint;
    protected override void GetPicked(Collider2D other)
    {
        Health otherHealth=other.transform.root.GetComponentInChildren<Health>();
        //if health missing is under or equal heal medkit can give
        if (otherHealth.HealthPoints <= otherHealth.MaxHealth - healthPoint)
        {
            otherHealth.Heal(healthPoint);
            Destroy(this.gameObject);
        }
       
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy"))
        {
            GetPicked(other);
            
        }
    }

}