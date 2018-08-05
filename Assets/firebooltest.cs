using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class firebooltest : MonoBehaviour
{

	public BooleanEvent BooleanEvent;
	
	public bool value;

	public bool fire;

	private void Update () 
	{
		if (fire)
		{
			fire = false;
			BooleanEvent.RaiseEvent(value);
		}
	}
}
