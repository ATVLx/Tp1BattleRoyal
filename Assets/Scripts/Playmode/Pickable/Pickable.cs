using System;
using System.Collections;
using System.Collections.Generic;
using Playmode.Util.Values;
using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
    protected abstract void OnCollisionEnter2D(Collision2D other);

    protected abstract void GetPicked(Collision2D other);

}
