using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class SpeedController : MonoBehaviour 
{
	public static SpeedController Instance{ get; private set;}
	public ObjectMove objectMove;
	public float currentNumberNode;

	public float CurrentNode
	{
		get 
		{
			return this.currentNumberNode;
		}
		set 
		{
			this.currentNumberNode = value;
			SetBallSpeed ();
		}
	}

	public float totalSpeed = 40f;
	public float TOTAL_NODE = 8;
	public float SPEED_CHANGE_NODE = 2; 
	private float changeSpeed;
	private Rigidbody rigidBody;

	void Awake()
	{
		if (Instance == null) 
		{
			Instance = this;
		} 
		else 
		{
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (Instance);
	}
		
	void Start()
	{
		rigidBody = objectMove.gameObject.GetComponent<Rigidbody> ();
		changeSpeed = totalSpeed /(TOTAL_NODE - SPEED_CHANGE_NODE);
	
	}

	public void StartRotation()
	{
		rigidBody.velocity = new Vector3(1.0f, 1.0f, 1.0f);
		totalSpeed = 40f;
		objectMove.ChangeSpeed (totalSpeed);
		objectMove.StartMove ();
	}

	public void StopRotation()
	{
		objectMove.Stop ();
	}

	public void ResetPosition()
	{
		objectMove.ResetToStart ();
	
	}

	private void SetBallSpeed()
	{
		//Debug.Log ("Set Ball Speed....");
		if ((CurrentNode - SPEED_CHANGE_NODE) > 0) 
		{
			totalSpeed = totalSpeed - changeSpeed;
			objectMove.ChangeSpeed (totalSpeed);
		}
	}

}
