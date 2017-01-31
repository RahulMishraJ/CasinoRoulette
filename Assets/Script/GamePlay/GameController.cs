using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roulette.GamePlay
{
	public class GameController : MonoBehaviour 
	{
		private static GameController _instance;

		public static GameController Instance
		{
			get
			{ 
				return INIT ();
			}
			private set{ }

		}

		public ConeController coneController;
		public RouletteRotation rouletteRotation;

		public static GameController INIT()
		{
			if(_instance == null)
			{
				GameObject obj = new GameObject ("GameController");
				_instance = obj.AddComponent<GameController> ();
			}
			return _instance;
		}

		void Awake()
		{
			if (_instance == null) 
			{
				_instance = this;
			} 
			else 
			{
				DestroyImmediate (this.gameObject);
			}
				
			//DontDestroyOnLoad (this);
		}
	}

}
