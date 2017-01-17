using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteAround : MonoBehaviour 
{
	public float rotationSpeed;
//	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (this.transform.position, -Vector3.up, Time.deltaTime*rotationSpeed);
	}
}
