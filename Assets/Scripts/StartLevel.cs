using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartLevel : MonoBehaviour
{

	public UnityEvent StartEvent;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "left")
		{
			StartEvent.Invoke();
		}
	}
}
