using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
	public bool Collectable = true;
	public BrickType BrickType;
	public List<SnapPositions> MySnapPositions;
}
