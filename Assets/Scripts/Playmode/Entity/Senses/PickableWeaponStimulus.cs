using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public class PickableWeaponStimulus : MonoBehaviour
    {
        private PickableWeapon weapon;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            weapon = transform.GetComponent<PickableWeapon>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<PickableWeaponSensor>()?.See(weapon);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<PickableWeaponSensor>()?.LooseSightOf(weapon);
        }
    }
}