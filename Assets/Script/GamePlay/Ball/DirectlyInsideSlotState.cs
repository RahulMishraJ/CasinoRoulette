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

	public enum HitState
	{
		None = 0,
		Obstacle,
	}


	private MovementState curMovementState;
	private HitState curHitState;

	private Rigidbody rigidbody;

	private Vector3 startPosition;

	private float reduceSpeedFactor;

	void Start()
	{
		reduceSpeedFactor = rotationSpeed / 500f;
		startPosition = transform.position;
		initialMovementTime = Random.Range (10f, 15f);
		rigidbody = this.GetComponent<Rigidbody> ();
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

			if (rotationSpeed > 0.2f) 
			{
				rotationSpeed = rotationSpeed - reduceSpeedFactor;
			}

			outerRadius = outerRadius - tempTimer;
			if (outerRadius < innerRadius) {
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
		rigidbody.AddRelativeTorque (tempdir*Time.deltaTime*20f);
		rigidbody.AddForce(-tempdir*Time.deltaTime*20f);

		//Debug.Log ("Distance....." + Vector3.Magnitude (transform.position - finalObject.transform.position));
		if (Vector3.Magnitude (transform.position - finalObject.transform.position) < 0.12f) {
			base.OnStageChanege ();
			rigidbody.isKinematic = true; 
		}


		//this.GetComponent<Rigidbody>().AddRelativeTorque (tempdir);
		//transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
		//transform.position = Vector3.Lerp (transform.position, finalObject.transform.position,Time.deltaTime*slotInsideMovementSpeed);
	}


	void OnTriggerEnter(Collider col)
	{
		if(this.enabled)
		{
			if (col.gameObject.tag.Equals ("Cone")) 
			{
				if (curHitState == HitState.Obstacle) 
				{
					rotationSpeed = 0;
					curMovementState = MovementState.InsideSlot;
					rigidbody.isKinematic = false; 
				}
			} 
			else if (col.gameObject.tag.Equals ("Obstacle")) 
			{
				Debug.Log ("Decrease Reduce Factor......");
				reduceSpeedFactor = rotationSpeed / 100f;
				curHitState = HitState.Obstacle;
			}
		}
		//base.OnStageChanege ();
	}

	void OnDisable()
	{
		//Reset ();
		//Invoke("Reset");
	}

	public void Reset()
	{
		curHitState = HitState.None;
		transform.position = startPosition;
		curMovementState = MovementState.Normal;
		outerRadius = 1.4f;
		rotationSpeed = 4f;
		reduceSpeedFactor = rotationSpeed / 500f;
		slotInsideMovementSpeed = 1f;
		timer = 0f;
		tempTimer = 0f;
		angle = 0;
		initialMovementTime = Random.Range (5f, 10f);
	}

}
