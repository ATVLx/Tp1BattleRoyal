using System;
using Playmode.Movement;
using Playmode.Weapon;
using UnityEditor;
using UnityEngine;

namespace Playmode.Ennemy.BodyParts
{
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
            if(gameObject.GetComponent<PickableWeapon>().Type.Equals(this.weapon.GetComponent<PickableWeapon>().Type))
            {
                switch (weapon.GetComponent<PickableWeapon>().Type)
                {
                        case PickableWeapon.WeaponType.Shotgun:
                            this.weapon.NbBullet += this.weapon.NbBullet;
                            break;
                        case PickableWeapon.WeaponType.Uzi:
                            this.weapon.FireDelayInSeconds /= 2;
                            break;
                }
                
            }
            else if (gameObject != null)
            {
                gameObject.transform.parent = transform;
                gameObject.transform.localPosition = Vector3.zero;
                
                weapon = gameObject.GetComponent<WeaponController>();
            }
            else
            {
                
                weapon = null;
            }
        }

        public void AimTowards(GameObject target)
        {
            //TODO : Utilisez ce que vous savez des vecteurs pour implémenter cette méthode
            throw new NotImplementedException();
        }

        public void Use()
        {
            if (weapon != null) weapon.Shoot();
        }
    }
}