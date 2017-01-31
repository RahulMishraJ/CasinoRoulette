﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Roulette.GamePlay;

namespace Roulette.UI
{
	public class PanelCell : PanelModule 
	{

		public override void OnClick (string _btn)
		{
			switch (_btn) 
			{
				case "Start":
				{
					base.NonClickableeButton ();
					GameController.Instance.rouletteRotation.rotationSpeed = 100f;
					GameController.Instance.rouletteRotation.Reset ();
					BallMovementController.Instance.StartBallMovemnt ();
				}
				break;
				case "Stop":
				{
					BallMovementController.Instance.curBallMovementState = BallMovementController.BallMovementState.Stop;
					BallMovementController.Instance.ChangeMovementState ();
				}
				break;
				case "Restart":
				{
					SceneManager.LoadScene(0);
				}
				break;
			}
		}

	}
}