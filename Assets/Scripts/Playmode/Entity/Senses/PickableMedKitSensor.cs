using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void PickableMedKitSensorEventHandler(PickableMedKit medKit);

    public class PickableMedKitSensor : MonoBehaviour
    {
        private List<PickableMedKit> medKitsInSight;
        public event PickableMedKitSensorEventHandler OnMedKitSeen;
        public event PickableMedKitSensorEventHandler OnMedKitSightLost;

        public List<PickableMedKit> MedKitInSight
        {
            get
            {
                medKitsInSight.RemoveAll(it => it == null);
                return medKitsInSight;
            }
        }

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            medKitsInSight = new List<PickableMedKit>();
        }

        public void See(PickableMedKit medKit)
        {
            medKitsInSight.Add(medKit);

            NotifyMedKitSeen(medKit);
        }


        public void LooseSightOf(PickableMedKit weapon)
        {
            medKitsInSight.Remove(weapon);

            NotifyMedKitSightLost(weapon);
        }

        private void NotifyMedKitSeen(PickableMedKit medKit)
        {
            if (OnMedKitSeen != null) OnMedKitSeen(medKit);
        }

        private void NotifyMedKitSightLost(PickableMedKit medKit)
        {
            if (OnMedKitSightLost != null) OnMedKitSightLost(medKit);
        }
    }
}