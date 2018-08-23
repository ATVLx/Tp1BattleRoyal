using System;
using UnityEngine;

namespace Playmode.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float fireDelayInSeconds = 1f;
        [Range(0, 360)][SerializeField] private float scatterAngle;
        [SerializeField]private int nbBullet;
        private float lastTimeShotInSeconds;

        private bool CanShoot => Time.time - lastTimeShotInSeconds > fireDelayInSeconds;

        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
        }

        private void ValidateSerialisedFields()
        {
            if (fireDelayInSeconds < 0)
                throw new ArgumentException("FireRate can't be lower than 0.");
        }

        private void InitializeComponent()
        {
            lastTimeShotInSeconds = 0;
        }

        public void Shoot()
        {
            if (CanShoot)
            {
                for (int i = 0; i < nbBullet; i++)
                {
                    GameObject bullet =Instantiate(bulletPrefab, transform.position, transform.rotation);
                    bullet.transform.Rotate(new Vector3(0,0,(i+1)*(scatterAngle/nbBullet-scatterAngle/2)));
                }
                

                lastTimeShotInSeconds = Time.time;
            }
        }
    }
}