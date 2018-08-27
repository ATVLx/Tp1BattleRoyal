﻿using UnityEngine;

namespace Playmode.Weapon
{
    public class ShotgunController : WeaponController
    {
        [Range(0, 360)][SerializeField] private float scatterAngle;
        [SerializeField]private int nbBullet=6;
        public override int NbBullet
        {
            get { return nbBullet; }
            set { nbBullet = value; }
        }
        public override void Shoot()
        {
           if (CanShoot)
            {
                for (int i = 0; i < nbBullet; i++)
                {
                    GameObject bullet =Instantiate(bulletPrefab, transform.position, transform.rotation);
                    bullet.transform.Rotate(new Vector3(0,0,(i+1)*(scatterAngle/nbBullet)-(scatterAngle/2)));
                }
                KnockBackRoot();

                lastTimeShotInSeconds = Time.time;
            }
        }
    }
}