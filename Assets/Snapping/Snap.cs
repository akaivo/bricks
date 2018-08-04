using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snap : MonoBehaviour
{

	public Brick Target;
	public float SnapDistance = 0.2f;
	public Transform SnappingBrick;

	private Vector3 _myPosInTargetSpace;
	private Quaternion _myRotInTargetSpace;
	
	private void Update ()
	{
		_myPosInTargetSpace = Target.transform.InverseTransformPoint(transform.position);
		_myRotInTargetSpace = Quaternion.Inverse(Target.transform.rotation) * transform.rotation;

		if (Target.MySnapPositions[0].Positions.All(OutsideSnapDistance))
		{
			SnappingBrick.localPosition = Vector3.zero;
			SnappingBrick.localRotation = Quaternion.identity;
		}
		else
		{
			SnapToBestPoint();
		}
	}

	private void SnapToBestPoint()
	{
		float min = float.MaxValue;
		Target.MySnapPositions[0].Positions.ForEach(p =>
		{
			float dPos = Vector3.Distance(p.LocalPosition, _myPosInTargetSpace);
			float dRot = Quaternion.Angle(p.LocalRotation, _myRotInTargetSpace);
			float d = 
		});
	}

	private bool OutsideSnapDistance(SnapPosition snapPoint)
	{
		return Vector3.Distance(_myPosInTargetSpace, snapPoint.LocalPosition) > SnapDistance;
	}
}
