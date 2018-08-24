using UnityEngine;

namespace Playmode.Application
{
    public class CameraEdge : MonoBehaviour
    {
        public float Height => 2f * Camera.main.orthographicSize;
        public float Width =>  Height * Camera.main.aspect;
    }
}