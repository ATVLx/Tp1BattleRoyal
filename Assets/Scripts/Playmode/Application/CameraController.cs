using Playmode.Application;
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
    private float shrinkAmmount;
    public event CameraEventHandler OnCameraEdgeChange;

    private void Start()
    {
        Debug.Log("camera Start");
        currentCameraSizeGoal = Camera.main.orthographicSize;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<EnnemiesSpawnedEventChannel>()
            .OnAllEnnemiesSpawned += OnAllEnnemySpawned;
    }

    public void OnAllEnnemySpawned()
    {
        float nbEnnemy= GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().PotentialWinners.Count;
        shrinkAmmount = (Camera.main.orthographicSize - minimumCameraSize) / nbEnnemy-1;
    }
    public void Shrink()
    {
        if (currentCameraSizeGoal != 10)
            currentCameraSizeGoal -= shrinkAmmount;
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
                GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraEventChannel>().AdaptGameToCamera();
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
        Shrink();
    }
    private void NotifyCameraEdgeChange()
    {
       if (OnCameraEdgeChange != null) OnCameraEdgeChange();
    }
}