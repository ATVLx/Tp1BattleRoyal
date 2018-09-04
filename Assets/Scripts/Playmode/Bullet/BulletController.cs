﻿using System;
using Playmode.Ennemy;
using Playmode.Entity.Destruction;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private float lifeSpanInSeconds = 5f;

        private Mover mover;
        private Destroyer destroyer;
        private float timeSinceSpawnedInSeconds;

        public EnnemyController Source { get; set; }

        private bool IsAlive => timeSinceSpawnedInSeconds < lifeSpanInSeconds;

        private void Awake()
        {
            ValidateSerialisedFields();
            InitialzeComponent();
        }

        private void ValidateSerialisedFields()
        {
            if (lifeSpanInSeconds < 0)
                throw new ArgumentException("LifeSpan can't be lower than 0.");
        }

        private void InitialzeComponent()
        {
            mover = GetComponent<RootMover>();
            destroyer = GetComponent<RootDestroyer>();
            Source = transform.root.GetComponentInChildren<EnnemyController>();

            timeSinceSpawnedInSeconds = 0;
        }

        private void Update()
        {
            UpdateLifeSpan();

            Act();
        }

        private void UpdateLifeSpan()
        {
            timeSinceSpawnedInSeconds += Time.deltaTime;
        }

        private void Act()
        {
            if (IsAlive)
                mover.Move(Mover.Foward);
            else
                destroyer.Destroy();
        }
    }
}