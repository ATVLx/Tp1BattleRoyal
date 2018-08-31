using Playmode.Bullet;
using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Weapon
{
    public class ShotgunController : WeaponController
    {
        [Range(0, 360)] [SerializeField] private float scatterAngle;
        [SerializeField] private bool randomizeBulletAngle;
        [SerializeField] private int nbBullet = 6;

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
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                    
                    bullet.GetComponentInChildren<BulletController>().Source =
                        transform.root.GetComponentInChildren<EnnemyController>();
                    bullet.GetComponentInChildren<BulletController>().Source = BulletSource;
                    float angle =randomizeBulletAngle
                            ? Random.Range(0f, scatterAngle) - (scatterAngle / 2)
                            : (i + 1) * (scatterAngle / nbBullet) - (scatterAngle / 2);
                    
                    bullet.transform.Rotate(new Vector3(0, 0, angle));
                }


                KnockBackRoot();

                lastTimeShotInSeconds = Time.time;
            }
        }
    }
}