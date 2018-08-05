using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayerOnBoolEvent : MonoBehaviour
{
    public BooleanEvent BooleanEvent;

    private void Awake()
    {
        BooleanEvent.AddListener(ReactToShow);
    }

    private void ReactToShow(bool value)
    {
        gameObject.layer = value ? LayerMask.NameToLayer("Default") : LayerMask.NameToLayer("Bricks");
    }

    private void OnDestroy()
    {
        BooleanEvent.RemoveListener(ReactToShow);
    }
}