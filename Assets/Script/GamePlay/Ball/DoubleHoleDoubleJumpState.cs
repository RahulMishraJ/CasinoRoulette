using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleHoleDoubleJumpState : BallMovement 
{
	public enum MovementState
	{
		Normal =0,
		PositionMatch,
		MoveUpSide,
		MoveDownSide,
		InsideSlotMoveUp,
		InsideSlotMoveDown,
		InsdieSlotMoveNormal,
		InsideSlotMove,

		None,
	}

	public enum HitState
	{
		None = 0,
		Obstacle,
	}

	public enum RadiusState
	{
		None =0,
		FisrtTime,
		SecondTime,
		ThirdTime,
		FourthTime,
	}


	public MovementState curMovementState;
	private HitState curHitState;
	public RadiusState curRadiusState; 

	private Rigidbody rigidbody;

	private Vector3 startPosition;

	private Vector3 lerpPositionUp;
	private Vector3 lerpPositionDown;

	private float decSpeedCollision = 2f;

	//-0.013,0.261, -1.4
	//-0.124,0.216,-1.398

	private float reduceSpeedFactor;

	void Start()
	{
		reduceSpeedFactor = rotationSpeed / 600f;

		startPosition = transform.position;
		initialMovementTime = Random.Range (10f, 15f);
		rigidbody = this.GetComponent<Rigidbody> ();
		hitPoint = 0.5f;
	}

	public  void FixedUpdate ()
	{
		FindYAxis ();
	//	Debug.Log ("Timer...."+timer);
		if (curMovementState == MovementState.Normal) {
			timer += Time.deltaTime*rotationSpeed;
			MoveAround ();
		} else if (curMovementState == MovementState.MoveUpSide) {
			timer += Time.deltaTime*rotationSpeed;
			MoveUpside ();
		} else if (curMovementState == MovementState.MoveDownSide) {
			timer += Time.deltaTime*rotationSpeed;
			MoveDownSide ();
		} else if (curMovementState == MovementState.InsideSlotMoveUp) {
			timer += Time.deltaTime*rotationSpeed;
			JumpUpSide ();
			//transform.position -= tempdir * Time.deltaTime * 0.2f;
			//MoveBallInsideSlot();
		} else if (curMovementState == MovementState.InsideSlotMoveDown) {
			timer += Time.deltaTime*rotationSpeed;
			JumpDownSide ();
		} else if (curMovementState == MovementState.InsdieSlotMoveNormal) {
			timer += Time.deltaTime*rotationSpeed;
			MoveInsideSlot ();
		} else if (curMovementState == MovementState.InsideSlotMove) {
			timer += Time.deltaTime*rotationSpeed;
			MoveBallInsideSlot ();
		} else if (curMovementState == MovementState.PositionMatch) {
			timer += Time.deltaTime*rotationSpeed;
			PositionMatch ();
		}

		//FindYAxis ();
	}

	public override void OnStageChanege ()
	{
		base.OnStageChanege ();
	}

	void MoveAround () 
	{
		Debug.Log ("Move Around....");
		//this.gameObject.
		//timer += Time.deltaTime*rotationSpeed;
		angle = timer;
		if (curRadiusState == RadiusState.FisrtTime) {
			tempTimer = timer;
			tempTimer = tempTimer / 400.0f;
			outerRadius = outerRadius - tempTimer;
			if (outerRadius < 1.25f) {
				//outerRadius = 1.2f;
				curRadiusState = RadiusState.SecondTime;
			}
		} else if (curRadiusState == RadiusState.SecondTime) {
			tempTimer = timer;
			tempTimer = tempTimer / 1000.0f;
			outerRadius = outerRadius + tempTimer;
			if (outerRadius < 1.35f) {
				curRadiusState = RadiusState.ThirdTime;
			}
		} else if (curRadiusState == RadiusState.ThirdTime) {
			tempTimer = timer;
			tempTimer = tempTimer / 10000.0f;
			outerRadius = outerRadius - tempTimer;

			rotationSpeed = rotationSpeed - reduceSpeedFactor;

			if (rotationSpeed < 0.8f)
				rotationSpeed = 0.8f;

//			if (outerRadius < 1.35f) {
//				curRadiusState = RadiusState.ThirdTime;
//			}
		} 
		else if (curRadiusState == RadiusState.FourthTime) 
		{
			//tempTimer = timer;
			tempTimer = radiusdec / 50f;
			outerRadius = outerRadius - tempTimer;
		}




		//Debug.Log ("angle...."+angle +"...."+(angle*180)/Mathf.PI);
		tempdir = Vector3.Normalize (transform.position -  new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius))));
		transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
		this.transform.position = new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius)));


	}

	void MoveUpside()
	{
		transform.position = Vector3.Lerp (transform.position, lerpPositionUp, Time.deltaTime*10f);
		if (Vector3.Magnitude (transform.position - lerpPositionUp) < 0.1f)
			curMovementState = MovementState.MoveDownSide;
		//Debug.Log ("Position....."+Vector3.Magnitude(transform.position - lerpPositionUp));
	}

	void MoveDownSide()
	{
		transform.position = Vector3.Lerp (transform.position, lerpPositionDown, Time.deltaTime*10f);
		if (Vector3.Magnitude (transform.position - lerpPositionUp) < 0.1f) {

//			GameObject obj = new GameObject ("Test");
//			angle = timer;
//			obj.transform.position = new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius)));


			curMovementState = MovementState.Normal;
		
			curRadiusState = RadiusState.FisrtTime;

			//curMovementState = MovementState.PositionMatch;
		}
	}

	void PositionMatch()
	{
		angle = timer;
	//	Debug.Log ("Pos-->>"+new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), 0.2f,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius))));
		transform.position = Vector3.Lerp (transform.position, new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), 0.2f,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius))), Time.deltaTime*15f);

		Debug.Log ("position...."+Vector3.Magnitude(transform.position - new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius),hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius)))));
		if(Vector3.Magnitude(transform.position - new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius)))) < 0.1f)
		{
			//curMovementState = MovementState.Normal;

			//curRadiusState = RadiusState.FisrtTime;

			curMovementState = MovementState.Normal;
			//curRadiusState = RadiusState.None;

		}


	}

	void JumpUpSide()
	{
		Debug.Log ("Jump up side....."+lerpPositionUp);
		//rotationSpeed =0.35f;
		rotationSpeed = 0.4f;
		transform.position = Vector3.Lerp (transform.position, lerpPositionUp, Time.deltaTime*10f);
		if (Vector3.Magnitude (transform.position - lerpPositionUp) < 0.05f) {
			lerpPositionDown = -tempdir * 0.15f + transform.position;
			lerpPositionDown.y = transform.position.y - 0.12f;
			curMovementState = MovementState.InsideSlotMoveDown;
			rotationSpeed = 0.6f;
		}
	
	}

	void JumpDownSide()
	{
		Debug.Log ("Jump down side....."+lerpPositionDown);
		transform.position = Vector3.Lerp (transform.position, lerpPositionDown, Time.deltaTime*10f);

		if (Vector3.Magnitude (transform.position - lerpPositionDown) < 0.01f) {
			outerRadius = 0.9f;
			//timer = Mathf.Asin ((transform.position.x - roulette.transform.position.x)/outerRadius);
		//	timer = 260*Mathf.PI/180;
			//Debug.Log ("Angle.>>>>...."+timer+"...."+Mathf.Asin ((roulette.transform.position.x - transform.position.x)/outerRadius));

			//Debug.Log ("coming inside....." + Vector3.Magnitude (transform.position - lerpPositionDown));
			//curMovementState = MovementState.None;


			//curMovementState = MovementState.Normal;
			//curRadiusState = RadiusState.None;
			//rotationSpeed = 0.1f;
			angle = timer;
			Debug.DrawLine (transform.position, new Vector3 ((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius), hitPoint,((roulette.transform.position.z + Mathf.Cos(angle) * outerRadius))), Color.yellow,20f);

			//curMovementState = MovementState.None;
			//curMovementState = MovementState.Normal;
			//curRadiusState = RadiusState.None;

			curMovementState = MovementState.PositionMatch;

			firsttimehit = false;
		}
	}
	Vector3 tempslot;

	void MoveInsideSlot()
	{
		Debug.Log ("move inside slot....");
		transform.position -= Vector3.forward * Time.deltaTime * 1f;
		tempslot = transform.position;
		tempslot.y += 0.001f;
		transform.position = tempslot; 
	
	}


	void MoveBallInsideSlot()
	{
		Debug.Log ("Move ball inside slot.......");
		tempdir = Vector3.Normalize (transform.position - finalObject.transform.position);
		rigidbody.AddRelativeTorque (tempdir*Time.deltaTime*20f);
		rigidbody.AddForce(-tempdir*Time.deltaTime*20f);
		//transform.position = Vector3.Lerp (transform.position, finalObject.transform.position,Time.deltaTime*2f);
		//Debug.Log ("Distance....." + Vector3.Magnitude (transform.position - finalObject.transform.position));
		if (Vector3.Magnitude (transform.position - finalObject.transform.position) < 0.12f) {

			Debug.Log ("complete......");
			base.OnStageChanege ();
			rigidbody.isKinematic = true; 
		}


		//this.GetComponent<Rigidbody>().AddRelativeTorque (tempdir);
		//transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
		//transform.position = Vector3.Lerp (transform.position, finalObject.transform.position,Time.deltaTime*slotInsideMovementSpeed);
	}

	public float hitPoint = 0.5f;

	void FindYAxis()
	{
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, - Vector3.up);

		if (Physics.Raycast (downRay, out hit,1000f)) 
		{
			hitPoint = hit.point.y ;
			Debug.LogError ("coming inside...");
			//Debug.DrawLine (transform.position, hit.point, Color.red, 20f);
		}
	}


	// 0.0277 0.2005 -1.3864
	// up  -0.9,0.1,0.0   down 0,0,0.0
	public bool firsttimehit = false;
	private float radiusdec;
	void OnTriggerEnter(Collider col)
	{
		if(this.enabled)
		{
//			lerpPosition.x = col.gameObject.transform.position.x - 0.1f;
//			lerpPosition.z = col.gameObject.transform.position.z - 0.1f;
//			lerpPosition.y = col.gameObject.transform.position.y + 0.05f;
			if (!firsttimehit) {
				if (col.gameObject.tag.Equals ("Knob")) {
					lerpPositionUp = new Vector3 (-0.013f, 0.261f, -1.4f);
					lerpPositionDown = new Vector3 (-0.124f, -0.0085f, -1.398f);
					rotationSpeed = rotationSpeed - decSpeedCollision;
					//rotationSpeed = 2f;
					curMovementState = MovementState.MoveUpSide;
				} else if (col.gameObject.tag.Equals ("Collider")) {
					//Debug.Log ("Slot........"+col.);
					if (!firsttimehit) {
						curRadiusState = RadiusState.None;
						curMovementState = MovementState.None;
						Debug.Log ("Inside trigger...." + tempdir + "position..." + transform.position);

						float angle1 = (Vector3.Angle (this.transform.position, roulette.transform.position) * 2 * Mathf.PI) / 180;
						Debug.Log ("Angle......"+angle1 + " ....x...."+(roulette.transform.position.x + Mathf.Sin(angle) * 0.87f) +"!!"+(roulette.transform.position.x + Mathf.Sin(angle+0.05f) * 0.87f));



//						firsttimehit = true;
//						curMovementState = MovementState.InsideSlotMoveUp;
//						lerpPositionDown = -tempdir * 0.01f;
//						lerpPositionDown.y = hitPoint;
//
//
//
						lerpPositionUp = -tempdir * 0.12f +transform.position;
						lerpPositionUp.y = 0.08f;


						lerpPositionDown = -tempdir * 0.24f + transform.position;
                    	lerpPositionDown.y = 0f;
						//lerpPositionUp.x += 0.03f;

						GameObject obj = new GameObject ("Test");
						obj.transform.position = lerpPositionUp;
						obj.transform.localScale = new Vector3 (0.04f,0.04f,0.04f);

						GameObject obj1 = new GameObject ("Test1");
						obj1.transform.position = lerpPositionDown;
						obj1.transform.localScale = new Vector3 (0.04f,0.04f,0.04f);



						Debug.Log ("lerp poistion up...."+lerpPositionUp+"....down...."+lerpPositionDown);

					
					}
				}
			}
			if (col.gameObject.tag.Equals ("Obstacle")) 
			{
				Debug.Log ("Obstacle......");
				curRadiusState = RadiusState.FourthTime;
				float radius = outerRadius - 0.9f;
				radiusdec = outerRadius - radius;

			}
			if (col.gameObject.tag.Equals ("Cone")) 
			{
				Debug.Log ("Cone......");
				rigidbody.isKinematic = false;
				curMovementState = MovementState.InsideSlotMove;
				//curMovementState = MovementState.None;
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
