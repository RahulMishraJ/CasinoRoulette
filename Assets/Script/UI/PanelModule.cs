using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Roulette.UI
{
	public abstract class PanelModule : MonoBehaviour 
	{
		public Button[] buttons;

		public virtual void OnClick(string _btn)
		{
			Debug.Log ("Base On click");
		}

		public virtual void NonClickableeButton()
		{
			for(int i = 0; i < buttons.Length ;i++ )
			{
				buttons [i].interactable = false;
			}
		}

		public virtual void ClickableButton()
		{
			for(int i = 0; i < buttons.Length ;i++ )
			{
				buttons [i].interactable = true;
			}
		}
	}
}
