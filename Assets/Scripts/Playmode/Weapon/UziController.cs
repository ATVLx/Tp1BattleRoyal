using UnityEngine;

namespace Playmode.Weapon
{
    public class UziController : WeaponController
    {
        [SerializeField] public float unaccuracyAngle = 5;

        public override void Shoot()
        {
            if (CanShoot)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.Rotate(new Vector3(0, 0, (Random.Range(0, 5) - (unaccuracyAngle / 2))));
                KnockBackRoot();
                lastTimeShotInSeconds = Time.time;
            }
        }
    }
}