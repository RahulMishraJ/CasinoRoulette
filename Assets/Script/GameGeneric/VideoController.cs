using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour 
{
	public static VideoController Instance { get; private set;}

	public MediaPlayerCtrl mediaPlayerCtrl;

	public GameObject videoPlaySurface;

	private WebGLMovieTexture tex;

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


	public void OnPlayVideo()
	{
		#if UNITY_WEBGL //&& !UNITY_EDITOR
		tex = new WebGLMovieTexture("StreamingAssets/EasyMovieTexture.mp4");
		videoPlaySurface.GetComponent<MeshRenderer>().material = new Material (Shader.Find("Diffuse"));
		videoPlaySurface.GetComponent<MeshRenderer>().material.mainTexture = tex;
		tex.Play();
		tex.loop = true;
		#elif !UNITY_WEBGL
		Debug.LogError("Play Video");
		mediaPlayerCtrl.Play ();
		mediaPlayerCtrl.m_bAutoPlay = true;
		#endif
	}

	public void OnPause()
	{
		#if UNITY_WEBGL //&& !UNITY_EDITOR
		tex.Pause ();
		#elif !UNITY_WEBGL
		mediaPlayerCtrl.Pause();
		#endif
	}

	public void OnResume()
	{
		
		#if UNITY_WEBGL
		tex.Play();
		#elif !UNITY_WEBGL
		mediaPlayerCtrl.Play();
		#endif
	}

	public void OnStop()
	{
		#if UNITY_WEBGL //&& !UNITY_EDITOR
		#elif !UNITY_WEBGL
		mediaPlayerCtrl.Stop();
		#endif
	}

	void Update()
	{
		#if UNITY_WEBGL //&& !UNITY_EDITOR
		if(tex != null)
		{
			tex.Update();
		}
		#endif
	}

}
