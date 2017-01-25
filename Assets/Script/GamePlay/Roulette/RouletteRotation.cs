using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteRotation : MonoBehaviour {

	public float rotationSpeed;
	private float reduceSpeedFactor;
	public bool canSpeedReduced;


	void Update () 
	{
		transform.RotateAround (this.transform.position, -Vector3.up, Time.deltaTime*rotationSpeed);
		if (canSpeedReduced) 
		{
			if (rotationSpeed > 20f) {
				rotationSpeed -= reduceSpeedFactor;
			} else {
				rotationSpeed = 20f;
			}
		}
	}

	public void ReduceSpeed()
	{
		reduceSpeedFactor = rotationSpeed /600f;
		canSpeedReduced = true;
	
	}

	public void Reset()
	{
		canSpeedReduced = false;
	}


}
