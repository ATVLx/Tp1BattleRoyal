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
            //Arme du même type en main.
            if (weapon?.GetComponent<WeaponController>().Type == gameObject?.GetComponent<WeaponController>().Type)
            {
                switch (weapon.GetComponent<WeaponController>().Type)
                {
                    case WeaponController.WeaponType.Shotgun:
                        this.weapon.NbBullet += gameObject.GetComponent<WeaponController>().NbBullet;
                        break;
                    case WeaponController.WeaponType.Uzi:
                        this.weapon.FireDelayInSeconds = weapon.FireDelayInSeconds / 2;
                        break;
                    case WeaponController.WeaponType.Sniper:
                        break;
                }

                Destroy(gameObject);
            }
            //Arme différente.
            else if (gameObject != null)
            {
                //Possède déjà une arme.
                if (weapon != null)
                    Destroy(weapon.gameObject);
                gameObject.transform.parent = transform;
                gameObject.transform.localPosition = Vector3.zero;
                gameObject.transform.localRotation = Quaternion.identity;
                weapon = gameObject.GetComponent<WeaponController>();
                if (this.transform.root.GetComponentInChildren<EnnemyController>())
                {
                    weapon.BulletSource = this.transform.root.GetComponentInChildren<EnnemyController>();
                }
            }
            else
            {
                weapon = null;
            }
        }

        public void Use()
        {
            if (weapon != null) weapon.Shoot();
        }
    }
}