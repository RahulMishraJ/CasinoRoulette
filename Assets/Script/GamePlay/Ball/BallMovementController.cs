using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementController : MonoBehaviour 
{
	public static BallMovementController Instance {
		get;
		private set;
	}

	public enum BallMovementState
	{
		None = 0,
		DirectlyInsideSlot,
		DoubleHoleDoubleJump,
		DoubleHoleSingleJump,
		SingleHoleDoubleJump,
		SingleHoleSingleJump,
		Stop,
	}

	public DirectlyInsideSlotState directlyInsideSlotState;
	public DoubleHoleDoubleJumpState doubleHoleDoubleJumpState;
	public DoubleHoleSingleJumpState doubleHoleSingleJumpState;
	public SingleHoleDoubleJumpState singleHoleDoubleJumpState;
	public SingleHoleSingleJumpState singleHoleSingleJumpState;

	public BallMovementState curBallMovementState;



	void Awake()
	{
		if (Instance == null) 
		{
			Instance = this;
		} 
		else 
		{
			DestroyImmediate (this, true);
		}
	
	}

	public  void Start () 
	{
		//StartBallMovemnt ();	
	}

	public void StartBallMovemnt()
	{
		curBallMovementState = (BallMovementState)Random.Range(1, 6);
		//AssignMovementState ();
		//directlyInsideSlotState.Int();
		//directlyInsideSlotState.enabled = true;

		//doubleHoleDoubleJumpState.Int();
		//doubleHoleDoubleJumpState.enabled = true;

		//singleHoleSingleJumpState.enabled = true;

		singleHoleDoubleJumpState.enabled = true;

	}

	public void ChangeMovementState()
	{
		switch (curBallMovementState) 
		{
			case BallMovementState.DirectlyInsideSlot:
			{
				directlyInsideSlotState.enabled = true;
			}
			break;
			case BallMovementState.DoubleHoleDoubleJump:
			{
				doubleHoleDoubleJumpState.enabled = true;
			}
			break;
			case BallMovementState.DoubleHoleSingleJump:
			{
				doubleHoleSingleJumpState.enabled = true;
			}
			break;
			case BallMovementState.SingleHoleDoubleJump:
			{
				singleHoleDoubleJumpState.enabled = true;
			}
			break;
			case BallMovementState.SingleHoleSingleJump:
			{
				singleHoleSingleJumpState.enabled = true;
			}
			break;
			case BallMovementState.Stop :
			{
				directlyInsideSlotState.enabled = false;
				doubleHoleDoubleJumpState.enabled = false;
				doubleHoleSingleJumpState.enabled = false;
				singleHoleDoubleJumpState.enabled = false;
				singleHoleSingleJumpState.enabled = false;

			}
			break;
		}
	}
		
}
