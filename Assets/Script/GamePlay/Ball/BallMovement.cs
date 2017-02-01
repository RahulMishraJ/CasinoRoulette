using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Roulette.UI;

namespace Roulette.GamePlay
{
	public abstract class BallMovement : MonoBehaviour 
	{
		public GameObject roulette;
		public GameObject finalObject;

		public Transform finalReachedPoint;
		public Transform parentModel;

		public Transform[] movePoint; 

		protected float outerRadius ;
		protected float innerRadius ;
		protected float rotationSpeed;
		protected float ballRollingSpeed;
		protected float slotInsideMovementSpeed;
		protected float initialMovementTime;
		protected float timer;
		protected float tempTimer;
		protected float angle;
		protected float hitPoint;

		protected Vector3 tempdir;
		protected Vector3 movePositionUp;
		protected Vector3 movePositionDown;
		protected Vector3 movePosition;

		protected BallHolder ballholder;

		protected KnobController knobController;

		public virtual void OnStageChanege()
		{
			Debug.LogError ("On State change");
			BallMovementController.Instance.curBallMovementState = BallMovementController.BallMovementState.Stop;
			BallMovementController.Instance.ChangeMovementState ();
			UIController.Instance.panelCell [0].ClickableButton ();
			GameController.Instance.cameramovementController.Int ();
		}

		public virtual void BallMovementCircle()
		{
			tempdir = Vector3.Normalize (transform.position -  new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius))));
			transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
			this.transform.position = new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius)));
		}

		// find the yHitpoint
		public void FindYAxis()
		{
			RaycastHit hit;
			Ray downRay = new Ray(transform.position, - Vector3.up);
			if (Physics.Raycast (downRay, out hit,1000f)) 
			{
				hitPoint = hit.point.y  +0.06f;
			}
		}

		public void AddRouletteChild()
		{
			this.transform.parent = roulette.transform;
		}

		public void DeAttachedChild()
		{
			this.transform.parent = parentModel;
		}
	}
}
