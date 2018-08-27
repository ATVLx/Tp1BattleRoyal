using UnityEngine;


public class CameraController : MonoBehaviour
{
	[SerializeField] private float minimumCameraSize = 10;
	[SerializeField] private float shrinkingSpeedPerSeconds;
	private float currentCameraSizeGoal;
	private int numberOfEnnemyAtStart;


	private void Start()
	{
		currentCameraSizeGoal = Camera.main.orthographicSize;
	}

	public void Shrink(int numberEnnemyRemaining)
	{
			
		if (currentCameraSizeGoal != minimumCameraSize)
			currentCameraSizeGoal -= (Camera.main.orthographicSize/numberEnnemyRemaining);
		if (currentCameraSizeGoal < minimumCameraSize)
			currentCameraSizeGoal= minimumCameraSize;
	}

	private void Update()
	{
		if (currentCameraSizeGoal <= Camera.main.orthographicSize)
		{
			Camera.main.orthographicSize -= shrinkingSpeedPerSeconds * Time.deltaTime;
		}
	}
	
}
