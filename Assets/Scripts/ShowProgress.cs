using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowProgress : MonoBehaviour
{

	public Level Level;
	public BooleanEvent PlayEvent;
	public BooleanEvent WinEvent;

	private void Awake()
	{
		PlayEvent.AddListener(ReactToPlay);
		WinEvent.AddListener(ReactToWin);
	}

	private void OnDisable()
	{
		PlayEvent.RemoveListener(ReactToPlay);
		WinEvent.RemoveListener(ReactToWin);
	}

	private void ReactToPlay(bool obj)
	{
		GetComponent<Renderer>().enabled = obj;
		if (obj)
		{
			StartCoroutine(Progress(Level.PlayTime));
		}
		else
		{
			StopAllCoroutines();
		}
	}

	private IEnumerator Progress(float levelPlayTime)
	{
		float elapsed = 0f;
		while (elapsed < levelPlayTime)
		{
			float lerpValue = Mathf.Lerp(0, 1, 1 - elapsed / levelPlayTime);
			transform.localScale = new Vector3(lerpValue,transform.localScale.y, transform.localScale.z);
			elapsed += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	private void ReactToWin(bool obj)
	{
		GetComponent<Renderer>().enabled = false;
	}
}
