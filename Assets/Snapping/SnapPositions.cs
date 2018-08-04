using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SnapPositions : ScriptableObject
{
    public BrickType ForBrickType;
    public List<SnapPosition> Positions;
}