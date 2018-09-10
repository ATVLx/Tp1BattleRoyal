using UnityEngine;

namespace Playmode.Application
{
    //BEN_REVIEW : Tout le monde a une classe comme ça dans son code. Ça vient de StackOverlow je parie ?
    public class CameraEdge : MonoBehaviour
    {
        public float Height => 2f * Camera.main.orthographicSize;
        public float Width => Height * Camera.main.aspect;
    }
}