using System;
using Playmode.Bullet;
using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public class HitStimulus : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private int hitPoints = 10;

        private void Awake()
        {
            ValidateSerializeFields();
        }

        private void ValidateSerializeFields()
        {
            if (hitPoints < 0)
                throw new ArgumentException("Hit points can't be less than 0.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Entity.Senses.HitSensor>())
            {
                other.GetComponent<Entity.Senses.HitSensor>()?.Hit(hitPoints,this.transform.root.GetComponentInChildren<BulletController>().source);
                Destroy(this.gameObject);
            }
        }
    }
}