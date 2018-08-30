using System;
using System.Collections;
using Playmode.Ennemy;
using UnityEngine;
using UnityEngine.Jobs;

namespace Playmode.Entity.Status
{
    public delegate void HealthEventHandler(EnnemyController controller);

    public class Health : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
         private int healthPoints;

        private bool invincible = false;
        public event HealthEventHandler OnDeath;

        public int MaxHealth => maxHealth;

        public int HealthPoints
        {
            get { return healthPoints; }
            private set
            {
                healthPoints = value < 0 ? 0 : value;

                if (healthPoints <= 0) 
                    NotifyDeath();
            }
        }

        private void Awake()
        {
            ValidateSerialisedFields();
            healthPoints = maxHealth;
        }

        private void ValidateSerialisedFields()
        {
            if (healthPoints < 0)
                throw new ArgumentException("HealthPoints can't be lower than 0.");
        }

        public void Hit(int hitPoints)
        {
            if(invincible==false)
            HealthPoints -= hitPoints;
        }

        public void Heal(int healPoints)
        {
            HealthPoints += healPoints;
        }

        public void Invincibility(int durationInSeconds)
        {
            StartCoroutine(InvincibilityRoutine(durationInSeconds));
        }

        private IEnumerator InvincibilityRoutine(int durationInSeconds)
        {
            invincible = true;
            yield return new WaitForSeconds(durationInSeconds);
            invincible = false;
        }
        private void NotifyDeath()
        {
            if (OnDeath != null) OnDeath(GetComponent<EnnemyController>());
        }
    }
}