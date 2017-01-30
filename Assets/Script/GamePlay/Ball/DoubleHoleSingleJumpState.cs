using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleHoleSingleJumpState : BallMovement 
{
	public enum MovementState
	{
		None =0,
		Normal,
		PositionMatch,
		InsideSlotMove,
		CurveMovement,
		InSlotMovement,
		JumpUpSide,
		MoveMiddle,
		MoveDown,
		FinalPosition,
	}

	public enum HitState
	{
		None = 0,
		Obstacle,
	}

	public enum RadiusState
	{
		None =0,
		InitialDecRadius,
		DecRadius,
		IncRadius,
		DecRadiusSpeed,
		DecRadiusSpeedInc,
	}
		

	public MovementState curMovementState;
	public HitState curHitState;
	public RadiusState curRadiusState; 

	private Rigidbody rigidbody;

	private Vector3 startPosition;


	private float decSpeedCollision = 2f;

	//-0.013,0.261, -1.4
	//-0.124,0.216,-1.398

	private float reduceSpeedFactor;

	public bool firsttimehit = false;
	private float radiusdec;
	private float timetake = 0.2f;

	void Start()
	{
		startPosition = transform.position;
		Int ();
	}

	public void Int()
	{
		curRadiusState = RadiusState.InitialDecRadius;
		curHitState = HitState.None;
		transform.position = startPosition;
		curMovementState = MovementState.Normal;
		outerRadius = 1.67f;
		rotationSpeed = 3.4f;
		reduceSpeedFactor = rotationSpeed / 350f;
		slotInsideMovementSpeed = 1f;
		timer = 0f;
		tempTimer = 0f;
		angle = 0;
		initialMovementTime = Random.Range (10f, 15f);
		rigidbody = this.GetComponent<Rigidbody> ();
		hitPoint = 2f;
		firsttimehit = false;

	}


	public  void FixedUpdate ()
	{
		FindYAxis ();
		timer += Time.deltaTime*rotationSpeed;
		if (curMovementState == MovementState.Normal) 
		{
			MoveAround ();
		} 
		else if (curMovementState == MovementState.InsideSlotMove) 
		{
			MoveBallInsideSlot ();
		}
		else if (curMovementState == MovementState.CurveMovement) 
		{
			MoveBallInCurve ();
		}
		else if (curMovementState == MovementState.InSlotMovement) 
		{
			MoveBallInSlot ();
		}
		else if (curMovementState == MovementState.JumpUpSide) 
		{
			JumpUpSide ();
		}
		else if (curMovementState == MovementState.MoveMiddle) 
		{
			MoveMiddleSide ();
		}
		else if (curMovementState == MovementState.MoveDown) {
			MoveDownSide ();
		}
		else if (curMovementState == MovementState.FinalPosition) {
			MoveFinalPoint ();
		}
			
	}

	#region Ball Movement
	//Ball movement in predefine radius 
	void MoveAround () 
	{

		angle = timer;
		if (curRadiusState == RadiusState.InitialDecRadius) {
			if (timer > initialMovementTime) 
			{
				if (!GameController.Instance.rouletteRotation.canSpeedReduced) 
				{
					GameController.Instance.rouletteRotation.ReduceSpeed ();
				}
				tempTimer = timer - initialMovementTime;
				tempTimer = tempTimer / 4000.0f;
			
				outerRadius = outerRadius - tempTimer;
				if (outerRadius < 1.6f) {
					outerRadius = 1.6f;
				}
			}
		
		}
		else if (curRadiusState == RadiusState.DecRadius) {
			tempTimer = timer;
			tempTimer = tempTimer / 400.0f;
			outerRadius = outerRadius - tempTimer;
			if (outerRadius < 1.45f) {
				//outerRadius = 1.2f;
				curRadiusState = RadiusState.IncRadius;
				if (rotationSpeed < 0.8f)
					rotationSpeed = 0.8f;
			}
		} else if (curRadiusState == RadiusState.IncRadius) {
			tempTimer = timer;
			tempTimer = tempTimer / 1000.0f;
			outerRadius = outerRadius + tempTimer;
			if (outerRadius < 1.55f) {
				curRadiusState = RadiusState.DecRadiusSpeed;
				rotationSpeed = rotationSpeed - reduceSpeedFactor;
				if (rotationSpeed < 0.8f)
					rotationSpeed = 0.8f;
			}
		} else if (curRadiusState == RadiusState.DecRadiusSpeed) {
			tempTimer = timer;
			tempTimer = tempTimer / 10000.0f;
			outerRadius = outerRadius - tempTimer;

			rotationSpeed = rotationSpeed - reduceSpeedFactor;

			if (rotationSpeed < 0.8f)
				rotationSpeed = 0.8f;

		} 
		else if (curRadiusState == RadiusState.DecRadiusSpeedInc) 
		{
			//tempTimer = timer;
			tempTimer = radiusdec / 60f;
			outerRadius = outerRadius - tempTimer;
		}

		if (outerRadius < 1.35f)
			outerRadius = 1.35f;
		
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
		curRadiusState = RadiusState.DecRadius;
	}

	// move ball in curve to enter in slot
	void MoveBallInCurve()
	{
		movePosition = movePoint [0].position;
		movePosition.y = hitPoint - 0.02f;
		transform.position = Vector3.Slerp(transform.position,movePosition, Time.deltaTime*rotationSpeed*4.0f);
		if (Vector3.Magnitude (transform.position - movePosition) < 0.2f) {
			curMovementState = MovementState.InSlotMovement;
		}

	}

	// enter in the slot
	void MoveBallInSlot()
	{
		movePosition = movePoint [1].position;
		movePosition.y = hitPoint - 0.02f;
		transform.position = Vector3.Slerp(transform.position,movePosition, Time.deltaTime*rotationSpeed*4.0f);

	}

	//when hitting the collider moveup , middle and down 
	void JumpUpSide()
	{
		transform.position = Vector3.MoveTowards (transform.position, ballholder.doubleJump[0].transform.position, Time.deltaTime*(0.6f));
		if (Vector3.Magnitude (transform.position - ballholder.doubleJump[0].transform.position) < 0.1f) {
			curMovementState = MovementState.MoveMiddle;
		}
	
	}


	void MoveMiddleSide()
	{
		transform.position = Vector3.MoveTowards (transform.position, ballholder.doubleJump[1].transform.position, Time.deltaTime*(0.6f));
		if (Vector3.Magnitude (transform.position - ballholder.doubleJump[1].transform.position) < 0.1f) {
			curMovementState = MovementState.MoveDown;
		}

	
	}

	void MoveDownSide()
	{
		movePosition = ballholder.doubleJump [2].transform.position;
		movePosition.y = hitPoint - 0.02f;
		transform.position = Vector3.MoveTowards (transform.position, movePosition, Time.deltaTime*(0.6f));
		if (Vector3.Magnitude (transform.position - movePosition) < 0.05f) {
			curMovementState = MovementState.InsideSlotMove;
			rigidbody.isKinematic = false;
		}
	}

	// move inside slot
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

	// move to the final point
	private void MoveFinalPoint()
	{
		movePosition = finalReachedPoint.position;
		movePosition.y = hitPoint -0.02f;
		tempdir = Vector3.Normalize (transform.position - movePosition);
		rigidbody.AddRelativeTorque (tempdir*Time.deltaTime*2f);
		transform.position = Vector3.Lerp(transform.position, movePosition, Time.deltaTime*0.6f);
		if (Vector3.Magnitude (transform.position - movePosition) < 0.05f)
		{
			curMovementState = MovementState.None;
			OnComplete ();
		}
	}



	private void OnComplete()
	{
		Debug.LogError ("On Complete....");
	}

	#endregion
	// find the yHitpoint
	void FindYAxis()
	{
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, - Vector3.up);

		if (Physics.Raycast (downRay, out hit,1000f)) 
		{
			hitPoint = hit.point.y  +0.06f;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		//Debug.Log ("On trigger enter.jump..."+col.gameObject.tag);
		if(this.enabled)
		{

			if (!firsttimehit) 
			{
				if (col.gameObject.tag.Equals (GameConstant.KNOB_COL)) 
				{
					movePositionUp = col.gameObject.GetComponent<KnobController> ().upPosition [1].position;
					movePositionDown = col.gameObject.GetComponent<KnobController> ().downPosition [1].position;
					rotationSpeed = 1.8f;
					curRadiusState = RadiusState.None;
					curMovementState = MovementState.None;
					OnMoveUp ();

				} 
				else if (col.gameObject.tag.Equals (GameConstant.POCKET_COL)) 
				{
					if (!firsttimehit) {
						Debug.LogError ("Inside collider");
						curRadiusState = RadiusState.None;
						curMovementState = MovementState.None;
						firsttimehit = true;
						ballholder = col.gameObject.GetComponent<BallHolder> ();
						curMovementState = MovementState.JumpUpSide;
			
					}
				}
				else if (col.gameObject.tag.Equals (GameConstant.OBSTACLE_COL)) 
				{
					if (curHitState == HitState.Obstacle) 
					{
						curRadiusState = RadiusState.None;
						curMovementState = MovementState.CurveMovement;
					}
					if (col.gameObject.name.Equals (GameConstant.OBSTACLE_NAME)) 
					{
						curHitState = HitState.Obstacle;
						if(rotationSpeed >= 2f )
						{
							reduceSpeedFactor = reduceSpeedFactor * 2.8f;
						}
						else if((rotationSpeed >= 1.5f) && (rotationSpeed < 2f))
						{
							reduceSpeedFactor = reduceSpeedFactor * 2.2f;
						}
						else if((rotationSpeed >= 1f) && (rotationSpeed < 1.5f))
						{
							reduceSpeedFactor = reduceSpeedFactor * 1.6f;
						}
					}
				} 
				else if (col.gameObject.tag.Equals (GameConstant.CONE_COL)) 
				{
					rigidbody.isKinematic = false;
					curMovementState = MovementState.InsideSlotMove;
				}
			}
		}
	}

		
	public override void OnStageChanege ()
	{
		base.OnStageChanege ();
	}

	void OnDisable()
	{

	}

}
