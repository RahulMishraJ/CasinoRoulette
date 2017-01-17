using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPS : MonoBehaviour
{
	public static FPS _Instance = null;

	public float updateInterval = 0.5F;
	private double lastInterval;
	private int frames = 0;
	public float fps;

	Text fpsText;

	void Awake ()
	{
		_Instance = this;
	}

	void Start ()
	{
		lastInterval = Time.realtimeSinceStartup;
		frames = 0;

		fpsText = this.GetComponent<Text> ();
	}

	void Update ()
	{
		++frames;
		float timeNow = Time.realtimeSinceStartup;
		if (timeNow > lastInterval + updateInterval) {
			fps = (float)(frames / (timeNow - lastInterval));
			frames = 0;
			lastInterval = timeNow;
		}
		fpsText.text = fps.ToString ("f2");

		// change color when fps drops less than 20.0f

		if (fps < 40.0f) {
			fpsText.color = Color.red;
		} else {
			fpsText.color = Color.white;
		}
			
	}
}
