using System;
using System.Runtime.Remoting.Messaging;
using Lexic;
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

        [SerializeField] private NormalStrategy[] ennemyStrategies;

        private static readonly Color[] DefaultColors =
        {
            Color.white, Color.black, Color.blue, Color.cyan, Color.green,
            Color.magenta, Color.red, Color.yellow, new Color(255, 125, 0, 255)
        };

        private GameController gameController;

        [SerializeField] private GameObject ennemyPrefab;
        [SerializeField] private Color[] colors = DefaultColors;


        private void Start()
        {
            ValidateSerialisedFields();
            gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
            SpawnEnnemies();
        }

        private void ValidateSerialisedFields()
        {
            if (ennemyPrefab == null)
                throw new ArgumentException("Can't spawn null ennemy prefab.");
            if (colors == null || colors.Length == 0)
                throw new ArgumentException("Ennemies needs colors to be spawned.");
            if (ennemyStrategies.Length == 0)
                throw new ArgumentException("Must have at least 1 strategy");
        }

        private void SpawnEnnemies()
        {
            var colorProvider = new LoopingEnumerator<Color>(colors);

            for (var i = 0; i < NumberOfEnnemies; i++)
                SpawnEnnemy(
                    CreateRandomSpawnPosition(),
                    ennemyStrategies[i % ennemyStrategies.Length],
                    colorProvider.Next()
                );
        }

        private void SpawnEnnemy(Vector3 position, NormalStrategy strategy, Color color)
        {
            GameObject ennemy = Instantiate(ennemyPrefab, position, Quaternion.identity);
            ennemy.transform.root.name = this.GetComponent<NameGenerator>().GetNextRandomName();
            ennemy.GetComponentInChildren<EnnemyController>().Configure(Instantiate(strategy), color);
            gameController.AddPotentialWinner(ennemy.transform);
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