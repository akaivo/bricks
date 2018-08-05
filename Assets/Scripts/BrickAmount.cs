using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAmount : MonoBehaviour
{

	public static Action<int> Changed;

	public static int Value { get; private set; }

	private void Start()
	{
		Value = 25;
		if(Changed != null)
		{
			Changed(Value);
		}
	}

	public static void Add(int amount)
	{
		Value += amount;
		if(Changed != null)
		{
			Changed(Value);
		}
	}
}
