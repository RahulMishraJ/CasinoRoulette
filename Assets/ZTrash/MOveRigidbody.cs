using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOveRigidbody : MonoBehaviour {

	public enum MovementType
	{
		None = 0,
		RotateAround ,
		CurveMovement,
		Stop,
	}


	public MovementType curMovementType;

	public GameObject _Object;

	public GameObject hitObject;

//
	public float timer;
	float angle;
	float rad = 7.5f;
	float centerx = 0.0f;
	float centery = 0.0f;
	float centerz = 0.0f;
	public float speed;

	Vector3 temp;
	Vector3 temp1;
	bool canCurve = true;

	void Start () {

	//	-0.6,0.4,4.7

	//	temp = new Vector3(-0.6f, 1.0f, 4.7f);

		//temp = hitObject.transform.position;

//		temp = Vector3.Normalize(hitObject.transform.position -  this.transform.position);
//		Debug.Log ("temp...."+temp);
//		temp = temp + transform.position;
//		temp.y = transform.position.y + 0.5f;
//		temp.y = temp.y + 0.5f;
//		temp1 = temp;
//		temp1.z = temp1.z + 0.5f;
//		temp1.y = temp1.y - 0.5f;
	}




	void FixedUpdate () 
	{
		transform.RotateAround (this.transform.position, Vector3.forward, Time.deltaTime*50);
		if (curMovementType == MovementType.RotateAround) {
			MoveAround ();
		} else if (curMovementType == MovementType.CurveMovement) {
			MoveCurve ();
		}
	}


	void MoveCurve()
	{
//		if(canCurve)
//		Debug.LogError ("mag...."+Vector3.Magnitude(transform.position - temp)+"!!!!"+canCurve);
//
//		if(!canCurve)
//			Debug.LogError ("mag1...."+Vector3.Magnitude(transform.position - temp1));


		if (canCurve) {
			transform.position = Vector3.MoveTowards (transform.position, temp, Time.deltaTime * 5f);
			if (Vector3.Magnitude (transform.position - temp) < 0.35f) { 
				canCurve = false;
			}
		} else if (!canCurve) {
			transform.position = Vector3.MoveTowards (transform.position, temp1, Time.deltaTime * 5f);
			if (Vector3.Magnitude (transform.position - temp1) < 0.35f) { 
				curMovementType = MovementType.Stop;
			}
		}


	}


	void MoveAround () 
	{
		timer += Time.deltaTime*speed;
		angle = timer;
		if (timer > 5.0f) 
		{
			float timer1 = timer - 10.0f;
			timer1 = timer1 / 4000.0f;
			speed = speed - speed / 500f;
			rad = rad - timer1;
			if (rad < 4f) {
				//speed = Vector3.Magnitude (_Object.transform.position - transform.position);
				rad = 4f;
			}
		}

		this.transform.position = new Vector3 ((centerx + Mathf.Sin(angle) * rad), centery,((centerz + Mathf.Cos(angle) * rad)));
		//transform.RotateAround (this.transform.position, Vector3.Normalize(transform.position - new Vector3 ((centerx + Mathf.Sin(angle) * rad), centery,((centerz + Mathf.Cos(angle) * rad)))), Time.deltaTime*500);
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("On trigger enter");
//		Debug.Log ("Tag...."+col.gameObject.tag);
//		if (col.gameObject.tag.Equals ("Slot")) {
//			curMovementType = MovementType.CurveMovement;
//			Debug.Log ("Hit point...."+col.gameObject);
//
//
//			temp = Vector3.Normalize(hitObject.transform.position -  this.transform.position);
//			Debug.Log ("temp...."+temp);
//			temp = temp + transform.position;
//			temp.y = transform.position.y + 0.5f;
//		}
	}

	void OnCollisionEnter(Collision collision )
	{

//		Debug.Log ("On collision enter...");
//		if (collision.gameObject.tag.Equals ("Slot")) {
//
//			Rigidbody rigid =  this.gameObject.GetComponent<Rigidbody> ();
//			rigid.isKinematic = true;
//			//rigid.
//
//
//			curMovementType = MovementType.CurveMovement;
//			Debug.Log ("Hit point...."+collision.contacts[0].point);
//
//
//			//temp = Vector3.Normalize(collision.contacts[0].point -  this.transform.position);
//			Debug.Log ("temp...."+temp);
//			temp = collision.contacts[0].point;
//
//			Debug.Log ("temp...."+temp);
//			temp.y = transform.position.y + 1.0f;
//			temp1 = temp;
//			temp1.y = temp1.y - 1.0f;
//
//		
//		}

	}


}
// -0.6,0.4,4.7