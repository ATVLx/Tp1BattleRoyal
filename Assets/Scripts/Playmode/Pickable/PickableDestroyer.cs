using Boo.Lang;
using UnityEngine;

namespace Playmode.Application
{
    public class PickableDestroyer : MonoBehaviour
    {
        public List<Pickable.Pickable> pickables = new List<Pickable.Pickable>();

        private void Start()
        {
            Camera.main.GetComponent<CameraController>().OnCameraEdgeChange += DestroyPickableOutOfMap;
        }

        public void DestroyPickableOutOfMap()
        {
            pickables.RemoveAll(it => it == null);
            List<GameObject> pickablesToDestroy=new List<GameObject>();
            foreach (Pickable.Pickable p in pickables)
            {
                if (IsPickableOutOfMap(p.transform.position))
                {
                    pickablesToDestroy.Add(p.gameObject);
                }
            }
            
            foreach (GameObject GO in pickablesToDestroy)
            {
                Destroy(GO);
            }
            pickables.RemoveAll(it => it == null);
        }

        private bool IsPickableOutOfMap(Vector2 position)
        {
            if (Mathf.Abs(position.y) <= Camera.main.GetComponent<CameraEdge>().Height / 2 &&
                Mathf.Abs(position.x) <= Camera.main.GetComponent<CameraEdge>().Width / 2)
            {
            
                return false;
            }
            return true;
        }
    }
}