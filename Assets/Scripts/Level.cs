using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using UnityEngine;

public class Level : MonoBehaviour
{

	public float ShowTime = 3f;

	public float PlayTime = 5f;

	public BooleanEvent ShowingEvent;

	public BooleanEvent PlayEvent;

	public BooleanEvent StoppedEvent;

	private bool _running;

	private void Awake()
	{
		StoppedEvent.AddListener(Stop);
	}

	private void OnDestroy()
	{
		StoppedEvent.RemoveListener(Stop);
	}

	public void StartLevel()
	{
		if (_running) return;
		_running = true;
		ShowingEvent.RaiseEvent(true);
		StartCoroutine(WaitAndSwitchToPlay());
	}

	private IEnumerator WaitAndSwitchToPlay()
	{
		yield return new WaitForSecondsRealtime(ShowTime);
		ShowingEvent.RaiseEvent(false);
		PlayEvent.RaiseEvent(true);
		StartCoroutine(WaitAndDoTimeout());
	}

	private IEnumerator WaitAndDoTimeout()
	{
		yield return new WaitForSecondsRealtime(PlayTime);
		PlayEvent.RaiseEvent(false);
		_running = false;
	}

	private void Stop(bool obj)
	{
		if (!_running) return;
		
		PlayEvent.RaiseEvent(false);
		_running = false;
	}
}
