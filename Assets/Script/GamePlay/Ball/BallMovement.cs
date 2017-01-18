using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BallMovement : MonoBehaviour 
{

	public GameObject roulette;
	public GameObject finalObject;

	public float outerRadius = 1.35f;
	public float innerRadius = 1.1f;
	public float rotationSpeed;
	public float ballRollingSpeed;
	public float slotInsideMovementSpeed;

	public float initialMovementTime;
	public float timer;
	protected float tempTimer;
	protected float angle;

	protected Vector3 tempdir;

	public virtual void OnStageChanege()
	{
		Debug.LogError ("On State change");
		BallMovementController.Instance.curBallMovementState = BallMovementController.BallMovementState.Stop;
		BallMovementController.Instance.ChangeMovementState ();
	}

}
