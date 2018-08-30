using System;
using Playmode.Movement;
using Playmode.Weapon;
using UnityEditor;
using UnityEngine;

namespace Playmode.Ennemy.BodyParts
{
    public delegate void PickableEventHandler();
    public class HandController : MonoBehaviour
    {
        private Mover mover;
        private WeaponController weapon;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            mover = GetComponent<AnchoredMover>();
        }

        public void Hold(GameObject gameObject)
        {
          
            //if new weapon is the same add a buff to the weapon in hand
            if (weapon?.GetComponent<WeaponController>().Type == gameObject?.GetComponent<WeaponController>().Type)
            {
                switch (weapon.GetComponent<WeaponController>().Type)
                {
                    case WeaponController.WeaponType.Shotgun:
                        this.weapon.NbBullet += gameObject.GetComponent<WeaponController>().NbBullet;
                        break;
                    case WeaponController.WeaponType.Uzi:
                        this.weapon.FireDelayInSeconds = weapon.FireDelayInSeconds/ 2;
                        break;
                    case WeaponController.WeaponType.Sniper:
                        this.weapon.KnockBackForce += gameObject.GetComponent<WeaponController>().KnockBackForce;
                        weapon.DamageModifier += 1;
                        break;
                }
                Destroy(gameObject);
            }
            //if the weapon is a new one
            else if (gameObject != null)
            {
                //if the hand already have a weapon destroy it or drop it on the ground
                if (weapon != null)
                    Destroy(weapon.gameObject);
                gameObject.transform.parent = transform;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation=Quaternion.identity;
                weapon = gameObject.GetComponent<WeaponController>();
            }
            else
            {
                weapon = null;
            }
        }
        public void AimTowards(GameObject target)
        {
        }

        public void Use()
        {
            if (weapon != null) weapon.Shoot();
        }
    }
}