using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Playmode.Pickable;

namespace Playmode.Entity.Senses
{
    public delegate void PickableWeaponSensorEventHandler(PickableWeapon weapon);

    public class PickableWeaponSensor : MonoBehaviour
    {
        private LinkedList<PickableWeapon> weaponsInSight;

        // private HashSet<PickableWeapon> weaponsInSight;
        public event PickableWeaponSensorEventHandler OnWeaponSeen;
        public event PickableWeaponSensorEventHandler OnWeaponSightLost;


        public LinkedList<PickableWeapon> WeaponsInSight
        {
            get
            {
                for(int i = weaponsInSight.Count-1 ; i> weaponsInSight.Count ;i--)
                {
                    if (weaponsInSight.ElementAt(i) == null)
                    {
                        weaponsInSight.Remove(weaponsInSight.ElementAt(i));
                    }
                }
                return weaponsInSight;
            }
        }

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            weaponsInSight = new LinkedList<PickableWeapon>();
        }

        public void See(PickableWeapon weapon)
        {
            if (!weaponsInSight.Contains(weapon))
            {
                weaponsInSight.AddLast(weapon);
                NotifyWeaponSeen(weapon);
            }
        }

        public void LooseSightOf(PickableWeapon weapon)
        {
            weaponsInSight.Remove(weapon);
            NotifyWeaponSightLost(weapon);
        }

        private void NotifyWeaponSeen(PickableWeapon weapon)
        {
            if (OnWeaponSeen != null) OnWeaponSeen(weapon);
        }

        private void NotifyWeaponSightLost(PickableWeapon weapon)
        {
            if (OnWeaponSightLost != null) OnWeaponSightLost(weapon);
        }
    }
}