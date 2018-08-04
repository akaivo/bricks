using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class SnapPointCreator : MonoBehaviour
{
	public Transform target;

	public SnapPositions SnapPositions;

	public int Counter;

	public bool Make;

	private void Update () 
	{
		if (Make)
		{
			Make = false;
			AddControlPoint();
		}
	}

	private void AddControlPoint()
	{
		Vector3 myRelPos = target.InverseTransformPoint(transform.position);
		Quaternion myRelRot = Quaternion.Inverse(target.rotation) * transform.rotation;

		SnapPosition newSnapPosition = ScriptableObject.CreateInstance<SnapPosition>();
		newSnapPosition.LocalPosition = myRelPos;
		newSnapPosition.LocalRotation = myRelRot;
		
		AssetDatabase.CreateAsset(newSnapPosition, "Assets/Snapping/Snap Points/Standard/Standard "+ Counter +".asset");
		AssetDatabase.SaveAssets();
		
		SnapPositions.Positions.Add(newSnapPosition);
		Counter++;
	}
}
