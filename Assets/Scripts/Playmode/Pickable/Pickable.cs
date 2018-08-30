using System;
using System.Collections;
using System.Collections.Generic;
using Playmode.Ennemy;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Pickable
{

    public abstract class Pickable : MonoBehaviour
    {
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
    }
}