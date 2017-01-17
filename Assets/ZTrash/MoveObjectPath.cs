using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class MoveObjectPath : MonoBehaviour 
{
	public PathManager pathManager;
	public GameObject _Object;
	private int count = 1;
	// Use this for initialization
	void Start ()
	{
		Debug.Log(pathManager.waypoints.Length);
		MoveObject ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void MoveObject()
	{
	//	Vector3[] path = new Vector3[] { origin,pt1.position,pt2.position,pt3.position,pt3.position,pt4.position,pt5.position,origin};
		Vector3[] path = new Vector3[] { pathManager.waypoints [0].position,pathManager.waypoints [1].position,pathManager.waypoints [2].position,pathManager.waypoints [3].position,
			pathManager.waypoints [4].position,pathManager.waypoints [5].position,pathManager.waypoints [6].position,pathManager.waypoints [7].position};
		LeanTween.move (_Object, path, 100f).setOrientToPath (true);//.setOnComplete(OnComplete);
	}

	void OnComplete()
	{
		if((pathManager.waypoints.Length - 1) > count)
		{
			count++;
			MoveObject ();
		}
	}

}
