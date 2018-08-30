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

        private NormalStrategy strategy;

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
            hitSensor.OnHit += OnHit;  //subscribe a l'evenement
        }

        private void Update()
        {
            strategy.Act();
        }

        private void OnDisable()
        {
            hitSensor.OnHit -= OnHit;;
        }

        public void Configure(NormalStrategy strategy, Color color)
        {
            body.GetComponent<SpriteRenderer>().color = color;
            sight.GetComponent<SpriteRenderer>().color = color;
            this.strategy = strategy;
            strategy.Init(mover,handController,sight);
            typeSign.GetComponent<SpriteRenderer>().sprite = strategy.sprite;
        }

        private void OnHit(int hitPoints , EnnemyController source) //la fonction de levenement
        {
           // Debug.Log("OW, I'm hurt! I'm really much hurt!!!");
            health.Hit(hitPoints);
           strategy.DefendModeEngaged(source); 
        }

        private void OnDeath(EnnemyController controller)
        {
            //Debug.Log("Yaaaaarggg....!! I died....GG.");

            destroyer.Destroy();
        }

    }
}