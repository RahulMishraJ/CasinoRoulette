using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollider : MonoBehaviour {


	// Update is called once per frame
	void Update () {
		FindYAxis ();
	}

	void FindYAxis()
	{
		RaycastHit hit;
		Ray downRay = new Ray(transform.position, - Vector3.up);

		//Debug.DrawLine (transform.position, Vector3.up, Color.yellow, 20f);

		if (Physics.Raycast (downRay, out hit,1000f)) 
		{
			Debug.LogError ("coming inside...");
			Debug.DrawLine (transform.position, hit.point, Color.red, 20f);

			//float hoverError = hoverHeight - hit.distance;

		}
	}


}
