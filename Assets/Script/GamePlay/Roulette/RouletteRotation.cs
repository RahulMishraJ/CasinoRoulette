using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteRotation : MonoBehaviour {

	public float rotationSpeed;

	void Update () 
	{
		transform.RotateAround (this.transform.position, -Vector3.up, Time.deltaTime*rotationSpeed);
	}
}
