using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette.Screen
{
	public class ScreenController : MonoBehaviour 
	{
		public Image[] images;
		public Transform[] positions;

		private Vector3 originalPosition;

		private int imageCount;
		private int previousCount;
		private float delay;


		void Start () 
		{
		
			Int ();
		}

		public void Int()
		{
			imageCount = 0;
			delay = 0.1f;
			originalPosition = images [imageCount].transform.position;
			OnImageMovementCenter ();
		}
	

		public void OnImageMovementCenter()
		{
			LeanTween.move (images [imageCount].gameObject, positions [0].position, 5f).setOnComplete (OnComplete).setDelay(delay);
		}

		private void OnComplete()
		{
			previousCount = imageCount;
			imageCount++;
			if (imageCount >= images.Length) 
			{
				imageCount = 0;
			}
			delay = 2f;
			ResetOriginalPosition (images [imageCount].transform, originalPosition);
			Invoke ("OnImageMovementSidePosition",delay);
			OnImageMovementCenter ();
		}

		private void ResetOriginalPosition(Transform obj, Vector3 pos)
		{
			obj.position = pos;
		}

		private void OnImageMovementSidePosition()
		{
			LeanTween.move (images [previousCount].gameObject, positions [1].position, 5f);
		}
	}
}
