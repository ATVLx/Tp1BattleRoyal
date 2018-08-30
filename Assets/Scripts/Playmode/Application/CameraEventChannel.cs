using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Application
{
	public delegate void CameraEventHandler();
	
	public class CameraEventChannel : MonoBehaviour
	{

		public event CameraEventHandler OnCameraChange;

		public void AdaptGameToCamera()
		{
			NotifyCameraChanged();
		}
		
		private void NotifyCameraChanged()
		{
			if (OnCameraChange != null) OnCameraChange();
		}
		

	}

}

