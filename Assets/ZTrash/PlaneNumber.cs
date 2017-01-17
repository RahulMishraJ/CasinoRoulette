using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneNumber : MonoBehaviour 
{

	public GameObject cone;
	//public GameObject sphere;
	public float angle;
	private float radius = 5;
	public float count = 0;
	private float speed = 2f;
	public bool canMove;
	public List<int> numberAngles = new List<int>();
	public int number;
	public float rotationSpeed = 50f;

	// Use this for initialization
	void Start () 
	{
		//LeanTween.rotate (this.gameObject, 360, 4, 10.0f);
		angle = 360f / 37f;
//		Debug.Log ("angle...."+angle);
//		for (int i = 20; i < 37; i++) 
//		{
//			float radians = i*angle * Mathf.Deg2Rad;
//			Debug.Log ("radians" + radians);
//			float x = Mathf.Cos (radians)*radius;
//			Debug.Log ("x....." + x);
//			float z = Mathf.Sin(radians)*radius;
//			Vector3 pos = new Vector3 (x, 0.1f, z);
//			Debug.DrawLine (new Vector3 (0, 0, 0), pos, Color.red, 50f);
//		}

	}

	void Update()
	{
		//transform.RotateAround (this.transform.position, Vector3.up, Time.deltaTime*rotationSpeed);
	}

	[ContextMenu("Rotate")]
	public void Movement()
	{
		//count ++;
		cone.transform.eulerAngles = new Vector3 (0, numberAngles[number] * angle, 0);
		//cone.transform.eulerAngles = new Vector3 (0, count * angle, 0);
	}

//	public void MovementSphere()
//	{
//		sphere.transform.position = Vector3.Slerp (sphere.transform.position, cone.transform.position, Time.deltaTime*speed);
//
//	}

}
