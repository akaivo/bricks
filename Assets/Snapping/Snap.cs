﻿using System.Collections;
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

	public bool TrySnapping(Brick brick)
	{
		PopulateTargets(SnapDistance, brick);

		if (_targets.Count == 0)
		{
			brick.transform.position = transform.position;
			brick.transform.rotation = transform.rotation;
			return false;
		}
		else
		{
			SnapToBestTarget(brick);
			return true;
		}
	}
	
	

	private List<Brick> PopulateTargets(float snapDistance, Brick brick)
	{
		_targets.Clear();
		Physics.OverlapSphere(transform.position, SnapDistance, SphereCastLayerMask.value).ForEach(c =>
		{
			Brick b = c.GetComponent<Brick>();
			if (b != null && b.BrickType == brick.BrickType)
			{
				_targets.Add(b);
			}
		});
		return _targets;
	}

	private void SnapToBestTarget(Brick brick)
	{
		float min = float.MaxValue;
		_targets.ForEach(target =>
		{
			_myPosInTargetSpace = target.transform.InverseTransformPoint(transform.position);
			_myRotInTargetSpace = Quaternion.Inverse(target.transform.rotation) * transform.rotation;
			
			SnapPositions sp = target.MySnapPositions.Find(snapPoints => snapPoints.ForBrickType == brick.BrickType); 
			sp.Positions.ForEach(p =>
			{
				float dPos = Vector3.Distance(p.LocalPosition, _myPosInTargetSpace);
				float dRot = Quaternion.Angle(p.LocalRotation, _myRotInTargetSpace) * Mathf.PI / 180f;
				float d = Mathf.Sqrt(dPos * dPos + dRot * dRot);
				if(d < min)
				{
					min = d;
					brick.transform.position = target.transform.TransformPoint(p.LocalPosition);
					brick.transform.rotation = target.transform.rotation * p.LocalRotation;
				}
			});
			
		});
	}
}
