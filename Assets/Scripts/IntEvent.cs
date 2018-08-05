using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntEvent : ScriptableObject
{
    private Action<int> Event;

    public void AddListener(Action<int> action)
    {
        Event += action;
    }
        
    public void RemoveListener(Action<int> action)
    {
        Event -= action;
    }

    public void RaiseEvent(int value)
    {
        if(Event != null)
        {
            Event(value);
        }
    }
}