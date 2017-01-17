using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Test : MonoBehaviour {
	
	private Texture2D tex;
	private Sprite spr;
	public Image img;
	public Sprite _sprite;

	void Start () 
	{

		img.sprite = ConvertSpriteToTexture (ConvertTextureToSprite(_sprite));
	}
	

	public Texture2D ConvertTextureToSprite(Sprite spr)
	{

		tex = new Texture2D( (int)_sprite.rect.width, (int)_sprite.rect.height );
		Color[] pixels = _sprite.texture.GetPixels(  (int)_sprite.textureRect.x, 
			(int)_sprite.textureRect.y, 
			(int)_sprite.textureRect.width, 
			(int)_sprite.textureRect.height );
		tex.SetPixels( pixels );
		tex.Apply();
		return tex;
	}


	public Sprite ConvertSpriteToTexture(Texture2D tex)
	{
		Rect rect = new Rect (0, 0, tex.width, tex.height);
		Vector2 pivot = new Vector2 (0.5f, 0.5f);
		spr = Sprite.Create (tex, rect, pivot);

		return spr;
	
	}



}
