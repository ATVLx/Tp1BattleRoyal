using System;
using Playmode.Application;
using Playmode.Ennemy.Strategies;
using Playmode.Util;
using Playmode.Util.Collections;
using UnityEngine;
using Random = System.Random;

namespace Playmode.Ennemy
{
    public class EnnemySpawner : MonoBehaviour
    {
        [SerializeField] private int NumberOfEnnemies = 10;

        private static readonly Color[] DefaultColors =
        {
            Color.white, Color.black, Color.blue, Color.cyan, Color.green,
            Color.magenta, Color.red, Color.yellow, new Color(255, 125, 0, 255)
        };

        private GameController gameController;

        private static readonly EnnemyStrategy[] DefaultStrategies =
        {
            EnnemyStrategy.Normal,
            EnnemyStrategy.Careful,
            EnnemyStrategy.Cowboy,
            EnnemyStrategy.Camper
        };

        [SerializeField] private GameObject ennemyPrefab;
        [SerializeField] private Color[] colors = DefaultColors;

        private void Awake()
        {
            ValidateSerialisedFields();
        }

        private void Start()
        {
            gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            SpawnEnnemies();
        }

        private void ValidateSerialisedFields()
        {
            if (ennemyPrefab == null)
                throw new ArgumentException("Can't spawn null ennemy prefab.");
            if (colors == null || colors.Length == 0)
                throw new ArgumentException("Ennemies needs colors to be spawned.");
           
        }

        private void SpawnEnnemies()
        {
            var stragegyProvider = new LoopingEnumerator<EnnemyStrategy>(DefaultStrategies);
            var colorProvider = new LoopingEnumerator<Color>(colors);

            for (var i = 0; i < NumberOfEnnemies; i++)
                SpawnEnnemy(
                    CreateRandomSpawnPosition(),
                    stragegyProvider.Next(),
                    colorProvider.Next()
                );
        }

        private void SpawnEnnemy(Vector3 position, EnnemyStrategy strategy, Color color)
        {
            GameObject ennemy = Instantiate(ennemyPrefab, position, Quaternion.identity);
            ennemy.GetComponentInChildren<EnnemyController>().Configure(strategy, color);
            gameController.AddPotentialWinner(ennemy.GetComponentInChildren<EnnemyController>());
        }

        private Vector2 CreateRandomSpawnPosition()
        {
            return new Vector2(
                UnityEngine.Random.Range(-Camera.main.GetComponent<CameraEdge>().Width / 2,
                    Camera.main.GetComponent<CameraEdge>().Width / 2),
                UnityEngine.Random.Range(-Camera.main.GetComponent<CameraEdge>().Height / 2,
                    Camera.main.GetComponent<CameraEdge>().Height / 2));
        }
    }
}