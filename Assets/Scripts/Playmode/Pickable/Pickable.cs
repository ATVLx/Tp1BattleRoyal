using System;
using System.Collections;
using System.Collections.Generic;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Pickable
{

    public abstract class Pickable : MonoBehaviour
    {
        protected abstract void OnTriggerEnter2D(Collider2D other);

        protected abstract void GetPicked(Collider2D other);
    }
}