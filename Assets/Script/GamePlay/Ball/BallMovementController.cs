using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roulette.GamePlay
{
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
			DoubleHoleSingleJump,
			SingleHoleDoubleJump,
			SingleHoleSingleJump,
			Stop,
		}

		public DirectlyInsideSlotState directlyInsideSlotState;
		public DoubleHoleSingleJumpState doubleHoleSingleJumpState;
		public SingleHoleDoubleJumpState singleHoleDoubleJumpState;
		public SingleHoleSingleJumpState singleHoleSingleJumpState;

		public BallMovementState curBallMovementState;

		public int stopNumber;

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
			curBallMovementState = (BallMovementState)Random.Range(1, 5);
			//curBallMovementState = BallMovementState.DirectlyInsideSlot;
			ChangeMovementState ();
			GameController.Instance.coneController.RotateCone (stopNumber);
		}

		public void ChangeMovementState()
		{
			switch (curBallMovementState) 
			{
				case BallMovementState.DirectlyInsideSlot:
				{
					directlyInsideSlotState.enabled = true;
					directlyInsideSlotState.Int ();
				}
				break;
				case BallMovementState.DoubleHoleSingleJump:
				{
					doubleHoleSingleJumpState.enabled = true;
					doubleHoleSingleJumpState.Int ();
				}
				break;
				case BallMovementState.SingleHoleDoubleJump:
				{
					singleHoleDoubleJumpState.enabled = true;
					singleHoleDoubleJumpState.Int ();
				}
				break;
				case BallMovementState.SingleHoleSingleJump:
				{
					singleHoleSingleJumpState.enabled = true;
					singleHoleSingleJumpState.Int ();
				}
				break;
				case BallMovementState.Stop :
				{
					directlyInsideSlotState.enabled = false;
					doubleHoleSingleJumpState.enabled = false;
					singleHoleDoubleJumpState.enabled = false;
					singleHoleSingleJumpState.enabled = false;

				}
				break;
			}
		}
	}
}
