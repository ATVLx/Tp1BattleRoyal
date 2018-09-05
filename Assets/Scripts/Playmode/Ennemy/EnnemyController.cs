using System;
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
        [Header("Body Parts")] [SerializeField]
        protected GameObject body;

        [SerializeField] protected GameObject hand;
        [SerializeField] protected GameObject sight;
        [SerializeField] protected GameObject typeSign;

        [Header("Behaviour")] [SerializeField] private GameObject startingWeaponPrefab;

        protected Health health;
        protected Mover mover;
        protected Destroyer destroyer;
        protected EnnemySensor ennemySensor;
        protected HitSensor hitSensor;
        protected HandController handController;
        protected PickableWeaponSensor weaponSensor;

        protected NormalStrategy strategy;

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
        }

        protected void CreateStartingWeapon()
        {
            handController.Hold(Instantiate(
                startingWeaponPrefab,
                Vector3.zero,
                Quaternion.identity
            ));
        }

        //Inscription à l'évenement.
        private void OnEnable()
        {
            hitSensor.OnHit += OnHit;
        }

        private void Update()
        {
            strategy.Act();
        }

        private void OnDisable()
        {
            hitSensor.OnHit -= OnHit;
        }

        public void Configure(NormalStrategy strategy, Color color)
        {
            body.GetComponent<SpriteRenderer>().color = color;
            sight.GetComponent<SpriteRenderer>().color = color;
            this.strategy = strategy;
            strategy.Init(mover, handController, sight);
            typeSign.GetComponent<SpriteRenderer>().sprite = strategy.sprite;
        }

        //La fonction de l'évenement.
        private void OnHit(int hitPoints, EnnemyController source)
        {
            strategy.SetThreat(source);
        }

        private void OnDeath(EnnemyController controller)
        {
            destroyer.Destroy();
        }
    }
}