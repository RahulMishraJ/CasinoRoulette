using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectlyInsideSlotState : BallMovement 
{
	public enum MovementState
	{
		Normal =0,
		InsideSlot,
	}

	private MovementState curMovementState;

	private Vector3 startPosition;


	void Start()
	{
		startPosition = transform.position;
		initialMovementTime = Random.Range (5f, 10f);
	}

	public  void FixedUpdate ()
	{
		if (curMovementState == MovementState.Normal) 
		{
			MoveAround ();
		}
		else if(curMovementState ==  MovementState.InsideSlot )
		{
			MoveBallInsideSlot();
		}
		//FindYAxis ();
	}

	public override void OnStageChanege ()
	{
		base.OnStageChanege ();
	}

	void MoveAround () 
	{
		//this.gameObject.
		timer += Time.deltaTime*rotationSpeed;
		angle = timer;
		if (timer > initialMovementTime) 
		{
			tempTimer = timer - initialMovementTime;
			tempTimer = tempTimer / 4000.0f;
			rotationSpeed = rotationSpeed - rotationSpeed / 5000f;
			outerRadius = outerRadius - tempTimer;
			if (outerRadius < innerRadius) {
				//speed = Vector3.Magnitude (_Object.transform.position - transform.position);
				outerRadius = innerRadius;
			}
		}

		tempdir = Vector3.Normalize (transform.position -  new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), 0.2f,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius))));
		transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
		this.transform.position = new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), 0.2f,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius)));


	}


	void FindYAxis()
	{
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, - Vector3.up);

		//Debug.DrawLine (transform.position, Vector3.up, Color.yellow, 20f);
	
		if (Physics.Raycast (downRay, out hit,1000f)) 
		{
			Debug.LogError ("coming inside...");
			Debug.DrawLine (transform.position, hit.point, Color.red, 20f);

			//float hoverError = hoverHeight - hit.distance;
		
		}
	}

	void MoveBallInsideSlot()
	{
		tempdir = Vector3.Normalize (transform.position - finalObject.transform.position);
		transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
		transform.position = Vector3.Lerp (transform.position, finalObject.transform.position,Time.deltaTime*slotInsideMovementSpeed);
	}


	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("On Trigger Enter");
		curMovementState = MovementState.InsideSlot;
		//base.OnStageChanege ();
	}

	void OnDisable()
	{
		Reset ();
	}

	void Reset()
	{
		transform.position = startPosition;
		curMovementState = MovementState.Normal;
		outerRadius = 1.4f;
		rotationSpeed = 2f;
		slotInsideMovementSpeed = 1f;
		timer = 0f;
		tempTimer = 0f;
		angle = 0;
		initialMovementTime = Random.Range (5f, 10f);
	}

}
