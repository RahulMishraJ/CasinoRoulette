using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallMovement : MonoBehaviour 
{

	public GameObject roulette;
	public GameObject finalObject;

	public Transform finalReachedPoint;

	public Transform[] movePoint; 

	public float outerRadius = 1.35f;
	public float innerRadius = 1.1f;
	public float rotationSpeed;
	public float ballRollingSpeed;
	public float slotInsideMovementSpeed;

	public float initialMovementTime;
	public float timer;
	protected float tempTimer;
	protected float angle;
	public float hitPoint;
	protected Vector3 tempdir;
	protected Vector3 movePositionUp;
	protected Vector3 movePositionDown;
	protected Vector3 movePosition;
	public BallHolder ballholder;

	protected KnobController knobController;

	public virtual void OnStageChanege()
	{
		Debug.LogError ("On State change");
		BallMovementController.Instance.curBallMovementState = BallMovementController.BallMovementState.Stop;
		BallMovementController.Instance.ChangeMovementState ();
	}

	public virtual void BallMovementCircle()
	{
		tempdir = Vector3.Normalize (transform.position -  new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius))));
		transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
		this.transform.position = new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius)));
	}


}
