using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickFinder : MonoBehaviour
{
    public HashSet<Brick> BricksInCapsule = new HashSet<Brick>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Bricks")) return;
        Brick brick = other.gameObject.GetComponent<Brick>();
        if (brick == null || !brick.Collectable) return;

        BricksInCapsule.Add(brick);
    }

    private void OnTriggerExit(Collider other)
    {
        Brick brick = other.gameObject.GetComponent<Brick>();
        if (brick == null) return;

        BricksInCapsule.Remove(brick);
    }

}