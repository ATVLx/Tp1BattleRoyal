﻿using System;
using Playmode.Application;
using Playmode.Ennemy.BodyParts;
using Playmode.Ennemy.Strategies;
using Playmode.Entity.Destruction;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Movement;
using UnityEngine;
using Playmode.Ennemy.Strategies;
namespace Playmode.Ennemy
{
    public class EnnemyController : MonoBehaviour
    {
        [Header("Body Parts")] [SerializeField] private GameObject body;
        [SerializeField] private GameObject hand;
        [SerializeField] private GameObject sight;
        [SerializeField] private GameObject typeSign;
        [Header("Type Images")] [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite carefulSprite;
        [SerializeField] private Sprite cowboySprite;
        [SerializeField] private Sprite camperSprite;
        [Header("Behaviour")] [SerializeField] private GameObject startingWeaponPrefab;

       
        
        private Health health;
        private Mover mover;
        private Destroyer destroyer;
        private EnnemySensor ennemySensor;
        private HitSensor hitSensor;
        private HandController handController;
        private PickableWeaponSensor weaponSensor;

        private IEnnemyStrategy strategy;

        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
            CreateStartingWeapon();
        }

        private void ValidateSerialisedFields()
        {
            if (body == null)
                throw new ArgumentException("Body parts must be provided. Body is missing.");
            if (hand == null)
                throw new ArgumentException("Body parts must be provided. Hand is missing.");
            if (sight == null)
                throw new ArgumentException("Body parts must be provided. Sight is missing.");
            if (typeSign == null)
                throw new ArgumentException("Body parts must be provided. TypeSign is missing.");
            if (normalSprite == null)
                throw new ArgumentException("Type sprites must be provided. Normal is missing.");
            if (carefulSprite == null)
                throw new ArgumentException("Type sprites must be provided. Careful is missing.");
            if (cowboySprite == null)
                throw new ArgumentException("Type sprites must be provided. Cowboy is missing.");
            if (camperSprite == null)
                throw new ArgumentException("Type sprites must be provided. Camper is missing.");
            if (startingWeaponPrefab == null)
                throw new ArgumentException("StartingWeapon prefab must be provided.");
        }

        private void InitializeComponent()
        {
            health = GetComponent<Health>();
            mover = GetComponent<RootMover>();
            destroyer = GetComponent<RootDestroyer>();

            var rootTransform = transform.root;
            ennemySensor = rootTransform.GetComponentInChildren<EnnemySensor>();
            hitSensor = rootTransform.GetComponentInChildren<HitSensor>();
            handController = hand.GetComponent<HandController>();
            weaponSensor = rootTransform.GetComponentInChildren<PickableWeaponSensor>();

      // strategy = new TurnAndShootStragegy(mover, handController);
            //strategy = new NormalStrategy(mover, handController,sight);
            //strategy=new CarefullStrategy(mover,handController,sight);
            //strategy=new CamperStrategy(mover,handController,sight);
            //strategy=new CowboyStrategy(mover,handController,sight);
        }

        private void CreateStartingWeapon()
        {
            handController.Hold(Instantiate(
                startingWeaponPrefab,
                Vector3.zero,
                Quaternion.identity
            ));
        }

        private void OnEnable()
        {
            ennemySensor.OnEnnemySeen += OnEnnemySeen;
            ennemySensor.OnEnnemySightLost += OnEnnemySightLost;
            hitSensor.OnHit += OnHit;  //subscribe a l'evenement
            health.OnDeath += OnDeath;
            weaponSensor.OnWeaponSeen += OnWeaponSeen;
            weaponSensor.OnWeaponSightLost += OnWeaponSightLost;
        }

        private void Update()
        {
            strategy.Act();
        }

        private void OnDisable()
        {
            ennemySensor.OnEnnemySeen -= OnEnnemySeen;
            ennemySensor.OnEnnemySightLost -= OnEnnemySightLost;
            hitSensor.OnHit -= OnHit;
            health.OnDeath -= OnDeath;
            weaponSensor.OnWeaponSeen -= OnWeaponSeen;
            weaponSensor.OnWeaponSightLost -= OnWeaponSightLost;
        }

        public void Configure(EnnemyStrategy strategy, Color color)
        {
            body.GetComponent<SpriteRenderer>().color = color;
            sight.GetComponent<SpriteRenderer>().color = color;

            switch (strategy)
            {
                case EnnemyStrategy.Careful:
                    typeSign.GetComponent<SpriteRenderer>().sprite = carefulSprite;
                   this.strategy=new CarefullStrategy(mover,handController,sight);
                    break;
                case EnnemyStrategy.Cowboy:
                    typeSign.GetComponent<SpriteRenderer>().sprite = cowboySprite;
                   this.strategy=new CowboyStrategy(mover,handController,sight);
                    break;
                case EnnemyStrategy.Camper:
                    typeSign.GetComponent<SpriteRenderer>().sprite = camperSprite;
                    this.strategy=new CamperStrategy(mover,handController,sight);
                    break;
                default:
                    typeSign.GetComponent<SpriteRenderer>().sprite = normalSprite;
                   this.strategy=new NormalStrategy(mover,handController,sight);
                    break;
            }
        }

        private void OnHit(int hitPoints) //la fonction de levenement
        {
           // Debug.Log("OW, I'm hurt! I'm really much hurt!!!");
            health.Hit(hitPoints);
        }

        private void OnDeath(EnnemyController controller)
        {
            //Debug.Log("Yaaaaarggg....!! I died....GG.");

            destroyer.Destroy();
        }

        private void OnEnnemySeen(EnnemyController ennemy)
        {
           // Debug.Log("I've seen an ennemy!! Ya so dead noob!!!");
        }

        private void OnEnnemySightLost(EnnemyController ennemy)
        {
            //
           // Debug.Log("I've lost sight of an ennemy...Yikes!!!");
        }

        private void OnWeaponSeen(PickableWeapon weapon)
        {
            //Debug.Log("I've found a weapon!! Ya'll motherfuckers dead!!!");
        }

        private void OnWeaponSightLost(PickableWeapon weapon)
        {
           // Debug.Log("I've lost sight of a weapon!! Rip me 2018-2018");
        }
    }
}