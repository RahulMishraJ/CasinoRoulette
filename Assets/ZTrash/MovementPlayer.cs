using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour 
{
	public BezierCurve bezireCurve;
	private int count = 0;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void PlayerMove()
	{
		//LeanTween.move (this.gameObject, [Vector3[10, 10, 1], 1f);
		//for (int i = 0; i < bezireCurve.points.Length - 1; i++) 
		//{
			
		//}
		//LeanTween.move(this.gameObject, new Vector3(10,0,0),2f);
		//LeanTween.move(this.gameObject,bezireCurve.points[count],20f).setOnComplete(OnComplete);
	}

	void OnComplete()
	{
		Debug.Log ("OnComplete");
		count++;
		if(bezireCurve.points.Length > count)
		PlayerMove ();
	}

}
