using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInterface : MonoBehaviour, IPointerDownHandler, ISelectHandler, IPointerEnterHandler
{
	#region IPointerEnterHandler implementation

	void IPointerEnterHandler.OnPointerEnter (PointerEventData eventData)
	{
		Debug.Log ("OnPointerEnter");
		//throw new NotImplementedException ();
	}

	#endregion

	#region ISelectHandler implementation

	void ISelectHandler.OnSelect (BaseEventData eventData)
	{
		Debug.Log ("On Select...");
	}

	#endregion

	public PanelCell panelcell;

	#region IPointerDownHandler implementation
	void IPointerDownHandler.OnPointerDown (PointerEventData eventData)
	{
		Debug.Log ("On pointer down");
		panelcell.OnPreesed ();
	}
	#endregion

}
