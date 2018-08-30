using System;
using System.Collections;
using System.Collections.Generic;
using Playmode.Application;
using Playmode.Ennemy;
using Playmode.Entity.Destruction;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Pickable
{

    public abstract class Pickable : MonoBehaviour
    {
        private void OnEnable()
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraEventChannel>().OnCameraChange += CheckIfOutOfBounds;
        }

        private void OnDisable()
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraEventChannel>().OnCameraChange -= CheckIfOutOfBounds;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ennemy"))
                GetPicked(other.transform.root.GetComponentInChildren<EnnemyController>());
        }

        protected abstract void GetPicked(EnnemyController other);
        
        /*
        protected abstract void OnTriggerEnter2D(Collider2D other);

        protected abstract void GetPicked(Collider2D other);
        */

        private void CheckIfOutOfBounds()
        {
            if (!(Mathf.Abs(transform.position.y) <= Camera.main.GetComponent<CameraEdge>().Height / 2 )&&
                !(Mathf.Abs(transform.position.x) <= Camera.main.GetComponent<CameraEdge>().Width / 2))
            {
                GetComponent<RootDestroyer>().Destroy();
            }   
        } 
    }
}