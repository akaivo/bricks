using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SnapPositions : ScriptableObject
{
    public BrickType BrickType;
    public List<SnapPosition> Positions;
}