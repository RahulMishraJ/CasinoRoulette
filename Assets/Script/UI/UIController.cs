using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Roulette.GamePlay;

namespace Roulette.UI
{
	public class UIController : MonoBehaviour 
	{

		public static UIController Instance;

		public PanelCell[] panelCell;


		public InputField input;

		void Awake()
		{
			if (Instance == null) 
			{
				Instance = this;
			} 
			else 
			{
				DestroyImmediate (this.gameObject);
			}
		}


		void Start () 
		{
			input.onEndEdit.AddListener (OnEndChange);
		}

		public void OnEndChange(string str)
		{
			BallMovementController.Instance.stopNumber = int.Parse(str);

		}

		public void HidePanel()
		{
		
		}

		public void ShowPanel()
		{
		
		}
	}
}
