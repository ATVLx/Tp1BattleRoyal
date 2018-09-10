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
            //BEN_CORRECTION : Constante.
            //                 De plus, CameraEventChannel devrait être obtenu au "Awake" et conservé en attribut.
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraEventChannel>().OnCameraChange +=
                OnCameraChange;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //BEN_CORRECTION : Constante.
            if (other.CompareTag("Ennemy"))
            {
                if (GetPicked(other.transform.root.GetComponentInChildren<EnnemyController>()))
                {
                    //BEN_CORRETION : Constante.
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraEventChannel>()
                        .OnCameraChange -= OnCameraChange;
                    
                    //BEN_CORRECTION : Il manque pas un "Destroy" à quelque part ici ?
                    //                 Ce que je constate, c'est que vous le faites dans les classes enfant. Tant qu'à utiliser
                    //                 le patron "Template Method", (ce que vous avez fait sans vous en rendre compte peut-être)
                    //                 (https://en.wikipedia.org/wiki/Template_method_pattern)
                    //                 autant mieux sortir tout ce qui est commun à tous les pickables dans la classe
                    //                 "Pickable".
                }
            }
        }

        protected abstract bool GetPicked(EnnemyController other);

        //BEN_CORRECTION : Le "Out of Bounds" destoy devrait être géré dans un autre composant à part, question de
        //                 réutilisabilité.
        private void OnCameraChange()
        {
            if (CheckIfOutOfBounds())
            {
                //BEN_CORRECTION : Constante.
                GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraEventChannel>().OnCameraChange -=
                    OnCameraChange;
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