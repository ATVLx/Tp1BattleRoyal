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
        private List<PickableWeapon> weaponsInSight;

        // private HashSet<PickableWeapon> weaponsInSight;
        public event PickableWeaponSensorEventHandler OnWeaponSeen;
        public event PickableWeaponSensorEventHandler OnWeaponSightLost;


        public List<PickableWeapon> WeaponsInSight
        { 
            get
            {
                weaponsInSight.RemoveAll(it => it == null);
                return weaponsInSight;
            }
        }

        private void Awake()
        {
            
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            weaponsInSight = new List<PickableWeapon>();
        }

        public void See(PickableWeapon weapon)
        {
            if (!weaponsInSight.Contains(weapon))
            {
                weaponsInSight.Add(weapon);
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