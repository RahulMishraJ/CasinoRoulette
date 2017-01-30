using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectlyInsideSlotState : BallMovement 
{
	public enum MovementState
	{
		Normal =0,
		InSlot,
		MoveUp,
		MoveDown,
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


	private float reduceSpeedFactor;
	private float minimumSpeed;

	private int count = 0;


	void Start()
	{
		startPosition = transform.position;
		Int ();
	}

	// initialisation

	public void Int()
	{
		
		ballRollingSpeed = 500f;
		innerRadius = 1.35f;
		hitPoint = 2f;
		rigidbody = this.GetComponent<Rigidbody> ();
		minimumSpeed = 0.4f;
		count = 0;
		curHitState = HitState.None;
		transform.position = startPosition;
		curMovementState = MovementState.Normal;
		outerRadius = 1.67f;
		rotationSpeed = 3.4f;
		reduceSpeedFactor = rotationSpeed / 200f;
		slotInsideMovementSpeed = 1f;
		timer = 0f;
		tempTimer = 0f;
		angle = 0;
		initialMovementTime = Random.Range (10f, 15f);
	}

	public  void FixedUpdate ()
	{
		timer += Time.deltaTime*rotationSpeed;
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


	#region Ball Movement
	//Ball movement in predefine radius 
	void MoveAround () 
	{
		//this.gameObject.
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

		base.BallMovementCircle ();

	}

	// when hit the knob the move up and down
	private void OnMoveUp()
	{
		LeanTween.move (this.gameObject, movePositionUp, 0.04f).setOnComplete(OnCompleteMoveUp);
	}

	private void OnMoveDown()
	{

		LeanTween.move (this.gameObject, movePositionDown, 0.04f).setOnComplete(OnCompleteMoveDown);
	}

	private void OnCompleteMoveUp()
	{
		OnMoveDown ();
	}

	private void OnCompleteMoveDown()
	{
		rotationSpeed = 2.5f;
		curMovementState = MovementState.Normal;
	}

	// Ball move in slot
	void MoveBallInSlot()
	{
		movePosition = slotInPosition.position;
		movePosition.y = hitPoint - 0.02f;
		tempdir = Vector3.Normalize (transform.position - movePosition);
		rigidbody.AddRelativeTorque (tempdir*Time.deltaTime*2f);
		transform.position = Vector3.Slerp(transform.position, movePosition, Time.deltaTime*1.2f);
		if (Vector3.Magnitude (transform.position - movePosition) < 0.15f) {
			curMovementState = MovementState.InsideSlot;

		}
	}

	// Ball movement inside slot
	void MoveBallInsideSlot()
	{
		movePosition = finalObject.transform.position;
		movePosition.y = hitPoint - 0.02f;
		tempdir = Vector3.Normalize (transform.position - movePosition);
		rigidbody.AddRelativeTorque (tempdir*Time.deltaTime*2f);
		transform.position = Vector3.Lerp(transform.position, movePosition, Time.deltaTime*0.8f);
		if (Vector3.Magnitude (transform.position - movePosition) < 0.3f) {
			curMovementState = MovementState.FinalPosition;
			rigidbody.isKinematic = true; 
		}

	}

	// Ball move to last point
	private void MoveFinalPoint()
	{
		movePosition = finalReachedPoint.position;
		movePosition.y = hitPoint - 0.02f;
		tempdir = Vector3.Normalize (transform.position - movePosition);
		rigidbody.AddRelativeTorque (tempdir*Time.deltaTime*2f);
		transform.position = Vector3.Lerp(transform.position, movePosition, Time.deltaTime*0.6f);
		if (Vector3.Magnitude (transform.position - movePosition) < 0.05f)
		{
			curMovementState = MovementState.None;
			OnComplete ();
		}
	}


	#endregion

	private void OnComplete()
	{
		base.OnStageChanege ();
	}


	public override void OnStageChanege ()
	{
		base.OnStageChanege ();
	}



	void OnTriggerEnter(Collider col)
	{
		if (this.enabled) 
		{
			if (col.gameObject.tag.Equals (GameConstant.CONE_COL)) 
			{
				if (curHitState == HitState.Obstacle) {
					rotationSpeed = 0;
					curMovementState = MovementState.InsideSlot;
					rigidbody.isKinematic = false; 
				}
			} 
			else if (col.gameObject.tag.Equals (GameConstant.INSIDE_OBSTACLE)) 
			{
				curMovementState = MovementState.InSlot;
			} 
			else if (col.gameObject.tag.Equals (GameConstant.OBSTACLE_COL)) 
			{
				count++;
				if (count == 1) {
					if (rotationSpeed > 2f) {
						reduceSpeedFactor = reduceSpeedFactor * 12f;
					} else if ((rotationSpeed > 1.4f) && (rotationSpeed < 2f)) {
						reduceSpeedFactor = reduceSpeedFactor * 10f;
					} else if ((rotationSpeed > 0.8f) && (rotationSpeed < 1.4f)) {
						reduceSpeedFactor = reduceSpeedFactor * 8f;
					} else if ((rotationSpeed > 0.6f) && (rotationSpeed < 0.8f)) {
						reduceSpeedFactor = reduceSpeedFactor * 4.8f;
					} else if ((rotationSpeed > 0.4f) && (rotationSpeed < 0.6f)) {
						reduceSpeedFactor = reduceSpeedFactor * 2.5f;
					} else if ((rotationSpeed > 0.3f) && (rotationSpeed < 0.4f)) {
						reduceSpeedFactor = reduceSpeedFactor * 0.85f;
					} else if ((rotationSpeed > 0.1f) && (rotationSpeed < 0.3f)) {
						reduceSpeedFactor = reduceSpeedFactor * 0.8f;
					} else if ((rotationSpeed > 0.085f) && (rotationSpeed < 0.1f)) {
						reduceSpeedFactor = reduceSpeedFactor * 0.55f;
					} else {
						reduceSpeedFactor = reduceSpeedFactor * 0.2f;
					}

				} else if (count == 2) {
					if (rotationSpeed > 0.7f) {
						reduceSpeedFactor = reduceSpeedFactor * 5f;
					}
					else if ((rotationSpeed > 0.5f) && (rotationSpeed < 0.7f)) {
						reduceSpeedFactor = reduceSpeedFactor * 3.5f;
					} 
				} else if (count == 3) {
					if (rotationSpeed > 0.5f) {
						reduceSpeedFactor = reduceSpeedFactor * 5f;
					}
					else if ((rotationSpeed > 0.3f) && (rotationSpeed < 0.5f)) {
						reduceSpeedFactor = reduceSpeedFactor * 3.5f;
					}
					minimumSpeed = 0.15f;
				}
				//reduceSpeedFactor = rotationSpeed*2f;
				curHitState = HitState.Obstacle;
			}
			else if (col.gameObject.tag.Equals (GameConstant.KNOB_COL)) 
			{
				curMovementState = MovementState.None;
				outerRadius = 1.5f;
				rotationSpeed = 2.0f;
				knobController = col.gameObject.GetComponent<KnobController> ();
				movePositionUp = knobController.upPosition [0].position;
				movePositionDown = knobController.downPosition [0].position;
				OnMoveUp ();
			}
		} 

	}

	// y hit point 

	void FindYAxis()
	{
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, - Vector3.up);

		if (Physics.Raycast (downRay, out hit,1000f)) 
		{
			hitPoint = hit.point.y  + 0.06f;
		}
	}

}
