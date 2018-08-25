using System;
using UnityEngine;

namespace Playmode.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected float fireDelayInSeconds = 1f;

        public virtual int NbBullet
        {
            get { return 1; }
            set { }
        }

        public enum WeaponType
        {
            Base,
            Shotgun,
            Uzi
        }

        [SerializeField] private WeaponType type;
        
        public WeaponType Type => type;
        

        public float FireDelayInSeconds
        {
            get { return fireDelayInSeconds; }
            set { fireDelayInSeconds = value; }
        }

        protected float lastTimeShotInSeconds;

        protected bool CanShoot => Time.time - lastTimeShotInSeconds > fireDelayInSeconds;

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

        public virtual void Shoot()
        {
            if (CanShoot)
            {

                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
              

                lastTimeShotInSeconds = Time.time;
            }
        }
    }
}