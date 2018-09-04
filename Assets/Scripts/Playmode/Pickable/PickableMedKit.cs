using Playmode.Ennemy;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Pickable;
using Playmode.Util.Values;
using UnityEngine;


public class PickableMedKit : Pickable
{
    [SerializeField] private int healthPoint;

    protected override bool GetPicked(EnnemyController other)
    {
        Health otherHealth = other.transform.root.GetComponentInChildren<Health>();
        //N'est pas ramsser si les points de vie sont trop haut.
        if (otherHealth.HealthPoints < otherHealth.MaxHealth)
        {
            otherHealth.Heal(healthPoint);
            Destroy(this.gameObject);
            return true;
        }

        return false;
    }
}