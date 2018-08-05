using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BooleanEvent : ScriptableObject
{
    private Action<bool> Event;

    public void AddListener(Action<bool> action)
    {
        Event += action;
    }
        
    public void RemoveListener(Action<bool> action)
    {
        Event -= action;
    }

    public void RaiseEvent(bool value)
    {
        if(Event != null)
        {
            Event(value);
        }
    }
}