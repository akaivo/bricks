using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnBoolEvent : MonoBehaviour
{
    public BooleanEvent BooleanEvent;
    public bool invert;
    public List<GameObject> GameObjects;

    private void Awake()
    {
        BooleanEvent.AddListener(ReactToShow);
    }

    private void ReactToShow(bool value)
    {
        GameObjects.ForEach(go =>
        {
            go.SetActive(value ^ invert);
        });
    }

    private void OnDestroy()
    {
        BooleanEvent.RemoveListener(ReactToShow);
    }
}