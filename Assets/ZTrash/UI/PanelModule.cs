using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class PanelModule : MonoBehaviour 
{
	public Button[] buttons;

//	public virtual void Update()
//	{
//		Debug.Log ("Updat parent........");
//	}
//

	public virtual void OnClick()
	{
		Debug.Log ("Base On click");
	}

}
