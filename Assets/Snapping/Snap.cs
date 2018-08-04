using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Snap : MonoBehaviour
{

	public float SnapDistance = 0.2f;
	public LayerMask SphereCastLayerMask;

	private Vector3 _myPosInTargetSpace;
	private Quaternion _myRotInTargetSpace;
	private readonly List<Brick> _targets = new List<Brick>();

	public Brick SnappingBrick;
	public bool IsSnapped { get; private set; }

	private void Update ()
	{
		PopulateTargets(SnapDistance);

		if (_targets.Count == 0)
		{
			SnappingBrick.transform.position = transform.position;
			SnappingBrick.transform.rotation = transform.rotation;
			IsSnapped = false;
		}
		else
		{
			SnapToBestTarget();
			IsSnapped = true;
		}
	}

	private List<Brick> PopulateTargets(float snapDistance)
	{
		_targets.Clear();
		Physics.OverlapSphere(transform.position, SnapDistance, SphereCastLayerMask.value).ForEach(c =>
		{
			Brick b = c.GetComponent<Brick>();
			if (b != null && b.BrickType == SnappingBrick.BrickType)
			{
				_targets.Add(b);
				Debug.Log(b.BrickType);
			}
		});
		return _targets;
	}

	private void SnapToBestTarget()
	{
		float min = float.MaxValue;
		_targets.ForEach(target =>
		{
			_myPosInTargetSpace = target.transform.InverseTransformPoint(transform.position);
			_myRotInTargetSpace = Quaternion.Inverse(target.transform.rotation) * transform.rotation;
			
			SnapPositions sp = target.MySnapPositions.Find(snapPoints => snapPoints.ForBrickType == SnappingBrick.BrickType); 
			sp.Positions.ForEach(p =>
			{
				float dPos = Vector3.Distance(p.LocalPosition, _myPosInTargetSpace);
				float dRot = Quaternion.Angle(p.LocalRotation, _myRotInTargetSpace) * Mathf.PI / 180f;
				float d = Mathf.Sqrt(dPos * dPos + dRot * dRot);
				if(d < min)
				{
					min = d;
					SnappingBrick.transform.position = target.transform.TransformPoint(p.LocalPosition);
					SnappingBrick.transform.rotation = target.transform.rotation * p.LocalRotation;
				}
			});
			
		});
	}

	private bool OutsideSnapDistance(SnapPosition snapPoint)
	{
		return Vector3.Distance(_myPosInTargetSpace, snapPoint.LocalPosition) > SnapDistance;
	}
}
