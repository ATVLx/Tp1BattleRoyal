using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Pickable;

namespace Playmode.Entity.Senses
{

    public delegate void PickableWeaponSensorEventHandler (PickableWeapon weapon);
    
    public class PickableWeaponSensor : MonoBehaviour
    {

        private HashSet<PickableWeapon> weaponsInSight;
        public event PickableWeaponSensorEventHandler OnWeaponSeen;
        public event PickableWeaponSensorEventHandler OnWeaponSightLost;

        public IEnumerable<PickableWeapon> WeaponsInSight
        {
            get
            {
                weaponsInSight.RemoveWhere(it => it == null);
                return weaponsInSight;
            }
        }
        
        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            weaponsInSight = new HashSet<PickableWeapon>();
        }

        public void See(PickableWeapon weapon)
        {
            weaponsInSight.Add(weapon);

            NotifyWeaponSeen(weapon);
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
