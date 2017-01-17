using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor {

	private BezierCurve curve;
	private Transform handleTransform;
	private Quaternion handleRotation;
	private Vector3[] points;

	private void OnSceneGUI () {
		curve = target as BezierCurve;
		handleTransform = curve.transform;
		handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;

		//Debug.Log ("curve point...."+curve.points.Length);

		points = new Vector3[curve.points.Length];
		for (int i = 0; i < (points.Length); i++) {
			points [i] = ShowPoint (i);
		}
		Handles.color = Color.red;
		for (int i = 0; i < (points.Length - 1); i++) {
			Handles.DrawLine(points [i], points [i+1]);
		}


//		Vector3 p0 = ShowPoint(0);
//		Vector3 p1 = ShowPoint(1);
//		Vector3 p2 = ShowPoint(2);
//
//		Handles.color = Color.red;
//		Handles.DrawLine(p0, p1);
//		Handles.DrawLine(p1, p2);
	}

	private Vector3 ShowPoint (int index) {
		Vector3 point = handleTransform.TransformPoint(curve.points[index]);
		EditorGUI.BeginChangeCheck();
		point = Handles.DoPositionHandle(point, handleRotation);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(curve, "Move Point");
			EditorUtility.SetDirty(curve);
			curve.points[index] = handleTransform.InverseTransformPoint(point);
		}
		return point;
	}
}