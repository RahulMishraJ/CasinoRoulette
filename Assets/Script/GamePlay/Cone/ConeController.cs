using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : MonoBehaviour 
{
	const float TOTAL_ANGLE = 360f;
	const float TOTAL_NUMBER = 37f;

	public GameObject obstacle;

	public List<int> numberAngles = new List<int>();

	private float eachNumberAngle;

	private float numeberSecondCone;

	void Start () 
	{
		eachNumberAngle = TOTAL_ANGLE / TOTAL_NUMBER;
	}

	// rotate cone to number as taken as input
	public void RotateCone(int inputNumber)
	{
		this.transform.localEulerAngles = new Vector3 (0, numberAngles[inputNumber] * eachNumberAngle, 0);
		OnRotateSecondCone (numberAngles[inputNumber]);
	}


		// going inside with jump
		private void OnRotateSecondCone(int number)
		{
			numeberSecondCone = number - 8;
			if (numeberSecondCone < 1) {
			
				numeberSecondCone = 36 + numeberSecondCone;
			}
			obstacle.transform.localEulerAngles = new Vector3 (0, numeberSecondCone * eachNumberAngle, 0);
		}





	// dirctly InsideSlot
//	private void OnRotateSecondCone(int number)
//	{
//		numeberSecondCone = number - 15;
//		if (numeberSecondCone < 1) {
//		
//			numeberSecondCone = 36 + numeberSecondCone;
//		}
//		obstacle.transform.localEulerAngles = new Vector3 (0, numeberSecondCone * eachNumberAngle, 0);
//	}

}
