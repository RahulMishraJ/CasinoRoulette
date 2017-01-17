using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : MonoBehaviour 
{
	const float TOTAL_ANGLE = 360f;
	const float TOTAL_NUMBER = 37f;

	public List<int> numberAngles = new List<int>();

	private float eachNumberAngle;

	void Start () 
	{
		eachNumberAngle = TOTAL_ANGLE / TOTAL_NUMBER;
	}

	// rotate cone to number as taken as input
	public void RotateCone(int inputNumber)
	{
		//this.transform.eulerAngles = new Vector3 (0, numberAngles[inputNumber] * eachNumberAngle, 0);
		this.transform.localEulerAngles = new Vector3 (0, numberAngles[inputNumber] * eachNumberAngle, 0);
		Debug.LogError ("Angles...."+numberAngles[inputNumber]);
	}

}
