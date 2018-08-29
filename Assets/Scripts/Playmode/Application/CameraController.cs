using UnityEditor;
using UnityEngine;

public delegate void CameraEventHandler();
public class CameraController : MonoBehaviour
{
    [SerializeField] private float minimumCameraSize = 10;
    [SerializeField] private float shrinkingSpeedPerSeconds;
    [SerializeField] private float followingCameraSize; 
    [SerializeField] private int followSpeed = 20;
    private float currentCameraSizeGoal;
    private int numberOfEnnemyAtStart;
    private bool following = false;
    private Transform followTransform;
    public event CameraEventHandler OnCameraEdgeChange;
   
    
    private void Start()
    {
        currentCameraSizeGoal = Camera.main.orthographicSize;
    }

    public void Shrink(int numberEnnemyRemaining)
    {
        if (currentCameraSizeGoal != minimumCameraSize)
            currentCameraSizeGoal -= (Camera.main.orthographicSize / numberEnnemyRemaining);
        if (currentCameraSizeGoal < minimumCameraSize)
            currentCameraSizeGoal = minimumCameraSize;
        if (following)
            currentCameraSizeGoal = followingCameraSize;
    }

    private void Update()
    {
        if (currentCameraSizeGoal < Camera.main.orthographicSize)
        {
            Camera.main.orthographicSize -= shrinkingSpeedPerSeconds * Time.deltaTime;
           if(currentCameraSizeGoal>=Camera.main.orthographicSize)
            {
                NotifyCameraEdgeChange();
            }
        }

        if (followTransform != null&&following)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position,
                new Vector3(followTransform.position.x, followTransform.position.y, -10), followSpeed * Time.deltaTime);
        }
    }

    public void StartFollowing(Transform transform)
    {
        following = true;
        followTransform = transform;
        Shrink(1);
    }
    private void NotifyCameraEdgeChange()
    {
       if (OnCameraEdgeChange != null) OnCameraEdgeChange();
    }
}