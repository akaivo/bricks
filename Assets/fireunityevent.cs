using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class fireunityevent : MonoBehaviour
{
    public bool fire;
    public UnityEvent UnityEvent;

    private void Update()
    {
        if (fire)
        {
            fire = false;
            UnityEvent.Invoke();
        }
    }
}