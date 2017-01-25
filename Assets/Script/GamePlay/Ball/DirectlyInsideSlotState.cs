using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectlyInsideSlotState : BallMovement 
{
	public enum MovementState
	{
		Normal =0,
		InSlot,
		InsideSlot,
		FinalPosition,
		None,
	}

	public enum HitState
	{
		None = 0,
		Obstacle,
	}

	public Transform slotInPosition;

	public MovementState curMovementState;
	private HitState curHitState;

	private Rigidbody rigidbody;

	private Vector3 startPosition;

	public float reduceSpeedFactor;

	private float minimumSpeed;

	void Start()
	{
		reduceSpeedFactor = rotationSpeed / 320f;//500
		startPosition = transform.position;
		minimumSpeed = 0.2f;
		initialMovementTime = Random.Range (10f, 15f);
		rigidbody = this.GetComponent<Rigidbody> ();
	}

	public  void FixedUpdate ()
	{
		if (curMovementState == MovementState.Normal) {
			MoveAround ();
		} else if (curMovementState == MovementState.InSlot) {
			MoveBallInSlot ();
		} else if (curMovementState == MovementState.InsideSlot) {
			MoveBallInsideSlot ();
		} else if (curMovementState == MovementState.FinalPosition) {
			MoveFinalPoint ();
		}
		FindYAxis ();
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
			if (!GameController.Instance.rouletteRotation.canSpeedReduced) 
			{
				GameController.Instance.rouletteRotation.ReduceSpeed ();
			}
			tempTimer = timer - initialMovementTime;
			tempTimer = tempTimer / 4000.0f;

			if (rotationSpeed > minimumSpeed) 
			{
				rotationSpeed = rotationSpeed - reduceSpeedFactor;
			}

			outerRadius = outerRadius - tempTimer;
			if (outerRadius < innerRadius) {
				outerRadius = innerRadius;
			}
		}

		tempdir = Vector3.Normalize (transform.position -  new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius))));
		transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
		this.transform.position = new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius)));


	}


	Vector3 tempPos;
	void MoveBallInsideSlot()
	{
		//Debug.Log ("MoveBallInsideSlot");
		tempPos = finalObject.transform.position;
		tempPos.y = hitPoint;
		tempdir = Vector3.Normalize (transform.position - tempPos);
		rigidbody.AddRelativeTorque (tempdir*Time.deltaTime*2f);
		//rigidbody.AddForce(-tempdir*Time.deltaTime*20f);
		transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime*0.6f);

		//Debug.Log ("Distance....." + Vector3.Magnitude (transform.position - tempPos));
		if (Vector3.Magnitude (transform.position - tempPos) < 0.3f) {
			curMovementState = MovementState.FinalPosition;
			rigidbody.isKinematic = true; 
			//MoveFinalPoint ();
			//this.transform.position = finalReachedPoint.position;
		}


		//this.GetComponent<Rigidbody>().AddRelativeTorque (tempdir);
		//transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
		//transform.position = Vector3.Lerp (transform.position, finalObject.transform.position,Time.deltaTime*slotInsideMovementSpeed);
	}

	private void MoveFinalPoint()
	{
		//Debug.Log ("MoveFinalPoint");
		tempPos = finalReachedPoint.position;
		tempPos.y = hitPoint;
		transform.position = Vector3.Lerp(transform.position, tempPos, Time.deltaTime*0.6f);
		//Debug.Log ("Distance....." + Vector3.Magnitude (transform.position - tempPos));
		if (Vector3.Magnitude (transform.position - tempPos) < 0.05f)
		{
				curMovementState = MovementState.None;		
		}
	}

	private void OnComplete()
	{
		base.OnStageChanege ();
	}


	void MoveBallInSlot()
	{
		//Debug.Log("Move ball in slot");
		tempPos = slotInPosition.position;
		tempPos.y = hitPoint;
		tempdir = Vector3.Normalize (transform.position - tempPos);
		rigidbody.AddRelativeTorque (tempdir*Time.deltaTime*2f);
		transform.position = Vector3.Slerp(transform.position, tempPos, Time.deltaTime*1f);

		//Debug.Log ("Distance....." + Vector3.Magnitude (transform.position - tempPos));
		if (Vector3.Magnitude (transform.position - tempPos) < 0.15f) {
			curMovementState = MovementState.InsideSlot;

		}


		//this.GetComponent<Rigidbody>().AddRelativeTorque (tempdir);
		//transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
		//transform.position = Vector3.Lerp (transform.position, finalObject.transform.position,Time.deltaTime*slotInsideMovementSpeed);
	}


	public int count = 0;

	void OnTriggerEnter(Collider col)
	{
		//Debug.Log ("OnTriggerEnter"+col.gameObject.tag);
		if(this.enabled)
		{
			if (col.gameObject.tag.Equals ("Cone")) {
				if (curHitState == HitState.Obstacle) {
					rotationSpeed = 0;
					curMovementState = MovementState.InsideSlot;
					rigidbody.isKinematic = false; 
					col.gameObject.GetComponent<MeshCollider> ().enabled = false;
				}
			} 
			else if (col.gameObject.tag.Equals ("InsideObstacle")) 
			{
			//	Debug.Log ("In slot");
				curMovementState = MovementState.InSlot;
				col.gameObject.GetComponent<MeshCollider> ().enabled = false;
			}
			else if (col.gameObject.tag.Equals ("Obstacle")) 
			{
				col.gameObject.GetComponent<MeshCollider> ().enabled = false;
				//Debug.Log ("Decrease Reduce Factor......");
				count++;
				if (count == 1) {
					if (rotationSpeed > 3f) {
						reduceSpeedFactor = reduceSpeedFactor * 8f;
					}
					else if ((rotationSpeed > 2.4f) && (rotationSpeed < 3f)) {
						reduceSpeedFactor = reduceSpeedFactor * 7f;
					}
					else if ((rotationSpeed > 1.8f) && (rotationSpeed < 2.4f)) {
						reduceSpeedFactor = reduceSpeedFactor * 6f;
					}
					else if ((rotationSpeed > 1.6f) && (rotationSpeed < 1.8f))

					{
						reduceSpeedFactor = reduceSpeedFactor * 2.8f;
					} else if ((rotationSpeed > 1.4f) && (rotationSpeed < 1.6f)) {
						reduceSpeedFactor = reduceSpeedFactor * 1.5f;
					} 
					else if ((rotationSpeed > 1.3f) && (rotationSpeed < 1.4f)) {
						reduceSpeedFactor = reduceSpeedFactor * 1.2f;
					}
					else if ((rotationSpeed > 1.1f) && (rotationSpeed < 1.3f)) {
						reduceSpeedFactor = reduceSpeedFactor * 0.8f;
					}
					else if ((rotationSpeed > 0.85f) && (rotationSpeed < 1.1f)) {
						reduceSpeedFactor = reduceSpeedFactor * 0.55f;
					}
					else {
						reduceSpeedFactor = reduceSpeedFactor * 0.2f;
					}

				} else if (count == 2) {
					if (rotationSpeed > 0.7f) {
						reduceSpeedFactor = reduceSpeedFactor * 1.2f;
					}
				}
				else if (count == 3) {
					if (rotationSpeed > 0.4f) {
						reduceSpeedFactor = reduceSpeedFactor * 1.2f;
					}
					minimumSpeed = 0.1f;
				}
				//reduceSpeedFactor = rotationSpeed*2f;
				curHitState = HitState.Obstacle;
			}
		}
		//base.OnStageChanege ();
	}

	void FindYAxis()
	{
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, - Vector3.up);

		if (Physics.Raycast (downRay, out hit,1000f)) 
		{
			hitPoint = hit.point.y  +0.02f;
			//Debug.LogError ("coming inside...");
			//Debug.DrawLine (transform.position, hit.point, Color.red, 20f);
		}
	}

	void OnDisable()
	{
		//Reset ();
		//Invoke("Reset");
	}

	public void Reset()
	{
		count = 0;
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
