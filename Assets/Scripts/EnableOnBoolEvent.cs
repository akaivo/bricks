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
        Enable(false);
    }

    private void ReactToShow(bool value)
    {
        Enable(value);
    }

    private void Enable(bool value)
    {
        GameObjects.ForEach(go => { go.SetActive(value ^ invert); });
    }

    private void OnDestroy()
    {
        BooleanEvent.RemoveListener(ReactToShow);
    }
}