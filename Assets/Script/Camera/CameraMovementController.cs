using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;
using Roulette.GamePlay;
using Roulette.UI;

namespace Roulette.Camera
{
	public class CameraMovementController : MonoBehaviour 
	{
		public CameraMovement cameraMovement;

		public Transform[] cameraWayPoint;

		private Vector3 originalPosition;
		private Vector3 originalAngle;
		private float focusSpeedTable = 4f;
		private float focusSpeedRoulette = 5f;

		void Start () 
		{
			originalPosition = this.transform.position;
			originalAngle = this.transform.eulerAngles;
			Int ();
		}

		public void Int()
		{
			
			this.transform.position = originalPosition;
			this.transform.eulerAngles = originalAngle;
			FocusTable ();
		}

		public void FocusTable()
		{
			LeanTween.move (this.gameObject, cameraWayPoint[0].position, focusSpeedTable);
		}

		public void FocusRoulette()
		{
			LeanTween.move (this.gameObject, cameraWayPoint[1].position, focusSpeedRoulette);
			LeanTween.rotate (this.gameObject,  cameraWayPoint[1].eulerAngles, focusSpeedRoulette).setOnComplete(OnCompleteFocusRoulette);
		}
			
		private void OnCompleteFocusRoulette()
		{
			UIController.Instance.panelCell [0].NonClickableeButton ();
			GameController.Instance.rouletteRotation.rotationSpeed = 100f;
			GameController.Instance.rouletteRotation.Reset ();
			BallMovementController.Instance.StartBallMovemnt ();
		}
	}
}
