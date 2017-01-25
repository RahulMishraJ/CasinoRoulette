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
		InsideSlotMoveMiddle,
		InsideSlotMoveDown,
		InsdieSlotMoveNormal,
		InsideSlotMove,
		SlerpMovement,
		SlerpMovementSecond,
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

	public Transform[] movePoint; 

	private Rigidbody rigidbody;

	private Vector3 startPosition;

	private Vector3 lerpPositionUp;
	private Vector3 lerpPositionDown;

	private float decSpeedCollision = 2f;

	//-0.013,0.261, -1.4
	//-0.124,0.216,-1.398

	private float reduceSpeedFactor;


	private float timetake = 0.2f;

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
		} 
		else if (curMovementState == MovementState.InsideSlotMoveMiddle) {
			timer += Time.deltaTime*rotationSpeed;
			JumpMiddleSide ();
		}
		else if (curMovementState == MovementState.InsideSlotMoveDown) {
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
		else if (curMovementState == MovementState.SlerpMovement) {
			timer += Time.deltaTime*rotationSpeed;
			MoveBall ();
		}
		else if (curMovementState == MovementState.SlerpMovementSecond) {
			timer += Time.deltaTime*rotationSpeed;
			MoveBallTwoPoint ();
		}
		//FindYAxis ();
	}

	public override void OnStageChanege ()
	{
		base.OnStageChanege ();
	}

	void MoveAround () 
	{
		//Debug.Log ("Move Around...."+angle);
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
			tempTimer = radiusdec / 60f;
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
		
	private int tweenID;

	void JumpUpSide()
	{
		Debug.Log ("Jump up side..start..."+ballholder.doubleJump[0].gameObject.name);
		tweenID = LeanTween.move (this.gameObject, ballholder.doubleJump [0].transform.position, .2f).setOnComplete (JumpMiddleSide).setEase(LeanTweenType.easeInCirc).id;
//		transform.position = Vector3.MoveTowards (transform.position, ballholder.doubleJump[0].transform.position, Time.deltaTime*0.8f);
//		Debug.Log ("differ"+Vector3.Magnitude(transform.position - ballholder.doubleJump[0].transform.position));
//		if (Vector3.Magnitude (transform.position - ballholder.doubleJump[0].transform.position) < 0.05f) {
//			Debug.Log ("Jump up side..complete...");
//
//			//curMovementState = MovementState.InsideSlotMoveMiddle;
//			//rotationSpeed = 0.6f;
//			JumpMiddleSide();
//		}
	
	}



	void JumpMiddleSide()
	{
		Debug.Log ("Jump middle side....start.");
		tweenID = LeanTween.move (this.gameObject, ballholder.doubleJump [1].transform.position, 0.2f).setOnComplete (JumpDownSide).setEase(LeanTweenType.easeInCirc).id;

//		Debug.Log ("Jump middle side..start...");
//		transform.position = Vector3.MoveTowards (transform.position, ballholder.doubleJump[1].transform.position, Time.deltaTime*0.8f);
//		if (Vector3.Magnitude (transform.position - ballholder.doubleJump[1].transform.position) < 0.01f) {
//			Debug.Log ("Jump middle side..complete...");
//			curMovementState = MovementState.InsideSlotMoveDown;
//		}
	
	}

	void JumpDownSide()
	{
		Debug.Log ("Jump down side....start.");
		tweenID = LeanTween.move (this.gameObject, ballholder.doubleJump [2].transform.position, timetake).setOnComplete (OnAllComplete).setEase(LeanTweenType.linear).id;

//		transform.position = Vector3.MoveTowards (transform.position, ballholder.doubleJump[2].transform.position, Time.deltaTime*0.8f);
//		tempdir = new Vector3(0.2f,0f, -0.5f);
//		tempdir.y = 0;
//		if (Vector3.Magnitude (transform.position - ballholder.doubleJump[2].transform.position) < 0.01f) {
//
//			Debug.Log ("Jump down side....complete.");
//			curMovementState = MovementState.InsdieSlotMoveNormal;
//			firsttimehit = false;
//		}
	}


	void OnAllComplete()
	{
		Debug.Log ("On Allcomplete...");
		firsttimehit = false;
		curMovementState = MovementState.InsdieSlotMoveNormal;
	}

	Vector3 tempslot;

	void MoveInsideSlot()
	{
		Debug.Log ("move inside slot...."+tempdir);
		//transform.position -= tempdir * Time.deltaTime * 0.2f;
		//curMovementState = MovementState.InsdieSlotMoveNormal;
		//tempslot = transform.position;
		//tempslot.y += 0.001f;
		//transform.position = tempslot; 
	
	}


	void MoveBallInsideSlot()
	{
		//Debug.Log ("Move ball inside slot.......");
		tempdir = Vector3.Normalize (transform.position - finalObject.transform.position);
		rigidbody.AddRelativeTorque (tempdir*Time.deltaTime*20f);
		tweenID = LeanTween.move (this.gameObject, finalObject.transform.position,2f).id;
		//rigidbody.AddForce(-tempdir*Time.deltaTime*20f);
		//rigidbody.useGravity = false;
		//transform.position = Vector3.Lerp (transform.position, finalObject.transform.position,Time.deltaTime*2f);
		//Debug.Log ("Distance....." + Vector3.Magnitude (transform.position - finalObject.transform.position));
		if (Vector3.Magnitude (transform.position - finalObject.transform.position) < 0.1f) {

			curMovementState = MovementState.None;
			Debug.Log ("complete......");
			base.OnStageChanege ();
			rigidbody.isKinematic = true; 
		}


		//this.GetComponent<Rigidbody>().AddRelativeTorque (tempdir);
		//transform.RotateAround (this.transform.position, tempdir , Time.deltaTime*ballRollingSpeed);
		//transform.position = Vector3.Lerp (transform.position, finalObject.transform.position,Time.deltaTime*slotInsideMovementSpeed);
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


	// 0.0277 0.2005 -1.3864
	// up  -0.9,0.1,0.0   down 0,0,0.0
	public bool firsttimehit = false;
	private float radiusdec;
	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("On trigger enter...."+col.gameObject.tag);
		if(this.enabled)
		{
//			lerpPosition.x = col.gameObject.transform.position.x - 0.1f;
//			lerpPosition.z = col.gameObject.transform.position.z - 0.1f;
//			lerpPosition.y = col.gameObject.transform.position.y + 0.05f;
			if (!firsttimehit) {
				if (col.gameObject.tag.Equals ("Knob")) {
//					lerpPositionUp = new Vector3 (-0.013f, 0.261f, -1.4f);
//					lerpPositionDown = new Vector3 (-0.124f, -0.0085f, -1.398f);

					lerpPositionUp = col.gameObject.GetComponent<KnobController> ().uPposition.position;
					lerpPositionDown  = col.gameObject.GetComponent<KnobController> ().downPosition.position;


					rotationSpeed = rotationSpeed - decSpeedCollision;
					//rotationSpeed = 2f;
					curMovementState = MovementState.MoveUpSide;



				} else if (col.gameObject.tag.Equals ("Collider")) {
					//Debug.Log ("Slot........"+col.);
					if (!firsttimehit) {
						LeanTween.cancel (tweenID);
						curRadiusState = RadiusState.None;
						curMovementState = MovementState.None;
						//Debug.Log ("Inside trigger...." + tempdir + "position..." + transform.position);
//
//						angle = timer;
//						float angle1 = (Vector3.Angle (this.transform.position, roulette.transform.position) * 2 * Mathf.PI) / 180;
//						Debug.Log ("angle..."+Vector3.Angle (this.transform.position, roulette.transform.position));
//						Debug.Log ("Angle......"+angle1 + " ....x...."+(roulette.transform.position.x + Mathf.Sin(angle) * outerRadius) +"!!"+(roulette.transform.position.x + Mathf.Sin(angle+0.05f) * outerRadius));



						firsttimehit = true;
//						curMovementState = MovementState.InsideSlotMoveUp;
//						lerpPositionDown = -tempdir * 0.01f;
//						lerpPositionDown.y = hitPoint;
//
//
//
//						lerpPositionUp = -tempdir * 0.12f +transform.position;
//						lerpPositionUp.y = 0.08f;
//
//
//						lerpPositionDown = -tempdir * 0.24f + transform.position;
//                    	lerpPositionDown.y = 0f;
//						//lerpPositionUp.x += 0.03f;
//
//						GameObject obj = new GameObject ("Test");
//						obj.transform.position = lerpPositionUp;
//						obj.transform.localScale = new Vector3 (0.04f,0.04f,0.04f);
//
//						GameObject obj1 = new GameObject ("Test1");
//						obj1.transform.position = lerpPositionDown;
//						obj1.transform.localScale = new Vector3 (0.04f,0.04f,0.04f);
//						angle -= 0.1f;
//						GameObject obj2 = new GameObject ("Test2");
//						obj2.transform.position = new Vector3((roulette.transform.position.x + Mathf.Sin(angle) * outerRadius) , hitPoint, (roulette.transform.position.x + Mathf.Sin(angle) * outerRadius));
//						obj2.transform.localScale = new Vector3 (0.04f,0.04f,0.04f);
					//	FindHitContact();
						ballholder = col.gameObject.GetComponent<BallHolder>();
						JumpUpSide ();
						//curMovementState = MovementState.InsideSlotMoveUp;
						curMovementState = MovementState.None;
						//Debug.Log ("lerp poistion up...."+lerpPositionUp+"....down...."+lerpPositionDown);

					
					}
				}
			}
			if (col.gameObject.tag.Equals ("Obstacle")) 
			{
				Debug.Log ("Obstacle......");
//				curRadiusState = RadiusState.FourthTime;
//				float radius = outerRadius - 0.9f;
//				radiusdec = outerRadius - radius;
				curRadiusState = RadiusState.None;
				curMovementState = MovementState.SlerpMovement;
				//MoveBall ();

			}
			if (col.gameObject.tag.Equals ("Cone")) 
			{
				Debug.Log ("Cone......");
				//LeanTween.cancel
				LeanTween.cancel (tweenID);
				rigidbody.isKinematic = false;
				curMovementState = MovementState.InsideSlotMove;
				//curMovementState = MovementState.None;
			}
		}
		//base.OnStageChanege ();
	}


	void MoveBall()
	{
		//Debug.Log ("Move Ball");
		transform.position = Vector3.Slerp(transform.position, movePoint[0].position, Time.deltaTime*3.0f);
		//Debug.Log ("transform.position "+Vector3.Magnitude(transform.position - movePoint[0].position));
		if (Vector3.Magnitude (transform.position - movePoint [0].position) < 0.2f) {
		
			curMovementState = MovementState.None;

			MoveBallTwoPoint();
		}//
		//tweenID = LeanTween.move (this.gameObject, movePoint.position,1f).setEase(LeanTweenType.easeInQuad ).id;
	
	}

	void MoveBallTwoPoint()
	{
		Debug.Log ("Move ball twopoint");
		//transform.position = Vector3.Slerp(transform.position, movePoint[1].position, Time.deltaTime*1f);
		tweenID = LeanTween.move (this.gameObject, movePoint[1].position,0.1f).setEase(LeanTweenType.linear ).id;
	}

	public LayerMask mask;

	public BallHolder ballholder;

	void FindHitContact()
	{
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, -tempdir);

		Debug.DrawLine (transform.position, -tempdir*1 , Color.yellow, 100f);

		if (Physics.Raycast (downRay, out hit,1000f,mask)) 
		{
			//hitPoint = hit.point.y ;

			ballholder = hit.collider.gameObject.GetComponent<BallHolder> ();
			//Debug.LogError ("coming contact..."+ballholder.gameObject.name);
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
