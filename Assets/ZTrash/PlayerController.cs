
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float torque;
	public float turn;
	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}


	public void Rotate()
	{
		Debug.Log ("Rotate......");
		//rb.AddTorque(transform.forward* torque * turn);
		rb.AddForce (new Vector3(1,0,1), ForceMode.Impulse);
	}


//	void FixedUpdate ()
//	{
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");
//
//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
//
//		rb.AddForce (movement * speed);
//	}
}
