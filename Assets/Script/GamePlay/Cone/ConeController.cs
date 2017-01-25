using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeController : MonoBehaviour 
{
	const float TOTAL_ANGLE = 360f;
	const float TOTAL_NUMBER = 37f;

	public GameObject [] obstacle;

	public List<int> numberAngles = new List<int>();

	private float eachNumberAngle;

	private float numeberSecondCone;

	private float val;

	void Start () 
	{
		eachNumberAngle = TOTAL_ANGLE / TOTAL_NUMBER;
	//	val = 
	}

	// rotate cone to number as taken as input
	public void RotateCone(int inputNumber)
	{
		this.transform.localEulerAngles = new Vector3 (0, numberAngles[inputNumber] * eachNumberAngle, 0);
		OnRotateSecondCone (numberAngles[inputNumber]);
	}


		// going inside with jump
//		private void OnRotateSecondCone(int number)
//		{
//		Debug.Log("number secondcone...."+number);
//			numeberSecondCone = number - 10;//8
//		Debug.Log("number secondcone...."+numeberSecondCone);
//
//			if (numeberSecondCone < 1) {
//			
//				numeberSecondCone = 37 + numeberSecondCone;
//			}
//			obstacle[0].transform.localEulerAngles = new Vector3 (0, numeberSecondCone * eachNumberAngle, 0);
//			obstacle[1].transform.localEulerAngles = new Vector3 (0, (numeberSecondCone+7) * eachNumberAngle, 0);
//		}
//




	// dirctly InsideSlot
	private void OnRotateSecondCone(int number)
	{
		numeberSecondCone = number - 15;
		if (numeberSecondCone < 1) {
		
			numeberSecondCone = 37 + numeberSecondCone;
		}
		obstacle[0].transform.localEulerAngles = new Vector3 (0, numeberSecondCone * eachNumberAngle, 0);
		obstacle[1].transform.localEulerAngles = new Vector3 (0, (numeberSecondCone+4) * eachNumberAngle, 0);
		obstacle[2].transform.localEulerAngles = new Vector3 (0, (numeberSecondCone+7) * eachNumberAngle, 0);
		obstacle[3].transform.localEulerAngles = new Vector3 (0, (numeberSecondCone+14) * eachNumberAngle, 0);
	}

}
