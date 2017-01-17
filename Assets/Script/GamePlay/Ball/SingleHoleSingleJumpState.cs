using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleHoleSingleJumpState : BallMovement 
{

	public void Update () 
	{
		Debug.Log ("child....");
	}

	public void FixedUpdate ()
	{

	}

	public override void OnStageChanege ()
	{
		base.OnStageChanege ();
	}

}
