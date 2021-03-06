﻿using Playmode.Ennemy.BodyParts;
using Playmode.Ennemy.Strategies;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy
{
    public partial class PlayerController : EnnemyController
    {
        [SerializeField] private KeyCode upKey;
        [SerializeField] private KeyCode downKey;
        [SerializeField] private KeyCode rightKey;
        [SerializeField] private KeyCode leftKey;
        [SerializeField] private KeyCode shootKey;
        [SerializeField] private PlayerStrategy playerStrategyPrefab;

        private void Start()
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddPotentialWinner(this);
            CreateStartingWeapon();
            strategy = playerStrategyPrefab;
            transform.root.name = "Player";
            typeSign.GetComponent<SpriteRenderer>().sprite = strategy.sprite;
        }

        void Update()
        {
            if (Input.GetKey(upKey))
            {
                mover.Move(Vector3.up * mover.Speed * Time.deltaTime);
            }

            if (Input.GetKey(downKey))
            {
                mover.Move(Vector3.down * mover.Speed * Time.deltaTime);
            }

            if (Input.GetKey(rightKey))
            {
                mover.Move(Vector3.right * mover.Speed * Time.deltaTime);
            }

            if (Input.GetKey(leftKey))
            {
                mover.Move(Vector3.left * mover.Speed * Time.deltaTime);
            }

            if (Input.GetKey(shootKey))
            {
                handController.Use();
            }

            //Angle en radians
            float AngleRad =
                Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.root.position.y,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.root.position.x);
            //Angle en Degrés
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotation
            this.transform.root.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
        }
    }
}