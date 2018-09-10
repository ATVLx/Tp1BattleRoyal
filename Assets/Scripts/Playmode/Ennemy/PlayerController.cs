using Playmode.Ennemy.BodyParts;
using Playmode.Ennemy.Strategies;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy
{
    //BEN_CORRECTION : Pas certain de votre hierachie. Il manque une classe intermédiaire entre les deux.
    //
    //                 Actuellement, vous êtes obligés d'avoir une stratégie vide juste à cause de votre joueur,
    //                 ce qui n'est pas normal.
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
            //BEN_CORRECTION : Constantes.
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddPotentialWinner(this);
            CreateStartingWeapon();
            strategy = playerStrategyPrefab;
            transform.root.name = "Player"; //BEN_CORRECTION : Constantes.
            typeSign.GetComponent<SpriteRenderer>().sprite = strategy.sprite;
        }

        //BEN_CORRECTION : Private manquant.
        void Update()
        {
            if (Input.GetKey(upKey))
            {
                //BEN_CORRECTION : Le calcul de vitesse se fait déjà dans les "Mover".
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

            //BEN_CORRECTION : Propreté.
            //Angle en radians
            float AngleRad =
                Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.root.position.y,
                    Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.root.position.x);
            //Angle en Degrés
            //BEN_REVIEW : Voir Mathf.Rad2Deg
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotation
            this.transform.root.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
        }
    }
}