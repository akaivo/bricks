using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SimpleEvent : ScriptableObject
{
    private Action Event;

    public void AddListener(Action action)
    {
        Event += action;
    }
        
    public void RemoveListener(Action action)
    {
        Event -= action;
    }

    public void RaiseEvent()
    {
        if(Event != null)
        {
            Event();
        }
    }
}