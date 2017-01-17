using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

	public enum MovementState
	{
		None =0,
		MoveCircular,
		MoveSphere,
		MoveInside
	}

	public MovementState curMovementState;

	// Use this for initialization
	public Transform cube;
	public float timer;
	float angle;
	float rad = 5.0f;
	float centerx = 0.0f;
	float centery = 0.0f;
	float centerz = 0.0f;
	float speed = 2.0f;

	void Start () 
	{
		Debug.Log ("Rotate around....");
		//LeanTween.rotateAround (this.gameObject, Vector3.zero , 360.0f, 10.0f);
		//LeanTween.move
	}
	
//	// move around
//	void MoveAround () 
//	{
//		timer += Time.deltaTime*speed;
//		angle = timer;
//		if (timer > 30.0f)
//			rad = 2.0f;
//		this.transform.position = new Vector3 ((centerx + Mathf.Sin(angle) * rad), centery,((centerz + Mathf.Cos(angle) * rad)));
//	}

//	void Update()
//	{
//		if (curMovementState == MovementState.MoveCircular) 
//		{
//			MoveAround();
//		}
//		else if (curMovementState == MovementState.MoveSphere) 
//		{
//			MoveInSphere();
//		}
//	}

//		void MoveAround () 
//		{
//			timer += Time.deltaTime*speed;
//			angle = timer;
//			if (timer > 5.0f) 
//			{
//				Vector3 direction = Vector3.Normalize (transform.position - cube.position);
//				Debug.Log ("Direction...."+direction);
//				curMovementState = MovementState.MoveSphere;
//				Vector3 temp = cube.position - direction*2;
//				cube.position = temp;
//			}
//		this.transform.position = new Vector3 ((cube.position.x + Mathf.Sin (angle) * rad), centery, ((cube.position.z + Mathf.Cos (angle) * rad)));
//		}

//	void MoveInSphere()
//	{
//
//		timer += Time.deltaTime*speed;
//		angle = timer;
//	//	if (timer > 25.0f)
//			//curMovementState = MovementState.MoveInside;
//		this.transform.position = new Vector3 ((cube.position.x + Mathf.Sin(angle) * rad), centery,((cube.position.z + Mathf.Cos(angle) * rad)));
//		//Vector3 direction = Vector3.Normalize (transform.position - cube.position);
//
//		//Debug.Log ("Direction...."+direction);
//
//		//		transform.position = Vector3.Slerp (transform.position, cube.position, Time.deltaTime*speed);
////		Debug.Log ("Distance....."+Vector3.Magnitude(transform.position - new Vector3(0,0,0)));
////		if ((Vector3.Magnitude (transform.position - new Vector3(0,0,0)))<2.5f ) 
////		{
////			curMovementState = MovementState.MoveInside;
////		}
//	}

}
