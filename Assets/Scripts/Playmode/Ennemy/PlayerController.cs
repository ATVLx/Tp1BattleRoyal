using System.Collections;
using System.Collections.Generic;
using Playmode.Ennemy;
using Playmode.Ennemy.BodyParts;
using Playmode.Movement;
using Playmode.Weapon;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Use this for initialization
    [SerializeField] private float speed;
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode shootKey;
    [SerializeField] private GameObject startingWeaponPrefab;
    private HandController handController;
    private RootMover mover;
    private 

    void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().AddPotentialWinner(this.transform);
        handController = transform.root.GetComponentInChildren<HandController>();
        handController.Hold(Instantiate(
            startingWeaponPrefab,
            Vector3.zero,
            Quaternion.identity));
        mover = transform.root.GetComponentInChildren<RootMover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(upKey))
        {
            mover.Move(Vector3.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(downKey))
        {
            mover.Move(Vector3.down * speed * Time.deltaTime);
        }

        if (Input.GetKey(rightKey))
        {
            mover.Move(Vector3.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(leftKey))
        {
            mover.Move(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(shootKey))
        {
            handController.Use();
        }
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.root.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.root.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.root.rotation = Quaternion.Euler(0, 0, AngleDeg-90);
    }
}