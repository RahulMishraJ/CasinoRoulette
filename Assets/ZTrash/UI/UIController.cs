using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

	public InputField input;


	void Start () 
	{
		input.onEndEdit.AddListener (OnEndChange);
	}

	public void OnEndChange(string str)
	{
		GameController.Instance.coneController.RotateCone (int.Parse(str));
	}

	public void OnClickRotate()
	{
		GameController.Instance.coneController.RotateCone (int.Parse(input.text));

	}

	public void OnStart()
	{
		GameController.Instance.rouletteRotation.rotationSpeed = 100f;
		GameController.Instance.rouletteRotation.Reset ();
		BallMovementController.Instance.StartBallMovemnt ();
	}

	public void OnStop()
	{
		BallMovementController.Instance.curBallMovementState = BallMovementController.BallMovementState.Stop;
		BallMovementController.Instance.ChangeMovementState ();
	}

	public void OnRestart()
	{
		BallMovementController.Instance.StartBallMovemnt ();
		//SceneManager.LoadScene(0);
	}

}
