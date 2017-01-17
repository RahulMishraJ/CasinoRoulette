using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCell : PanelModule 
{

//	public override void Update ()
//	{
//		Debug.Log ("Child Update");
//		//base.Update ();
//	}

	public override void OnClick ()
	{
		Debug.Log ("This Button Name....."+gameObject.name);

		base.OnClick ();
	}

	public void OnPreesed()
	{
		Debug.Log ("coming here...");
		SpriteState _spriteState = new SpriteState ();
		Sprite spr = this.GetComponent<Button>().spriteState.pressedSprite;

		buttons[0].GetComponent<Image>().sprite = spr;

		Resources.UnloadAsset (spr);
		//DestroyImmediate (spr,true);

	}

}
