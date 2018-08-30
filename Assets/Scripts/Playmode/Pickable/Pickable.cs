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
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraEventChannel>().OnCameraChange +=
                OnCameraChange;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ennemy"))
            {
                if (GetPicked(other.transform))
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraEventChannel>().OnCameraChange -=OnCameraChange;
                }
            }
        }

        protected abstract bool GetPicked(Transform other);

        private void OnCameraChange()
        {
            if (CheckIfOutOfBounds())
            {
                GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraEventChannel>().OnCameraChange -=OnCameraChange;
                Destroy(this.gameObject);
            }
        }

        private bool CheckIfOutOfBounds()
        {
            if (Mathf.Abs(transform.position.y) <= Camera.main.GetComponent<CameraEdge>().Height / 2 &&
                Mathf.Abs(transform.position.x) <= Camera.main.GetComponent<CameraEdge>().Width / 2)
            {
                return false;
            }

            return true;
        }
    }
}