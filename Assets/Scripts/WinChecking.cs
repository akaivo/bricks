﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WinChecking : MonoBehaviour
{

	public List<Brick> LevelBricks;
	public BoxCollider PlayVolume;
	public LayerMask PlayerBricksLayerMask;
	public BooleanEvent PlayEvent;
	public BooleanEvent StoppedEvent;
	public int WinMultiplier = 10;
	private void Awake()
	{
		PlayEvent.AddListener(React);
	}

	private void OnDestroy()
	{
		PlayEvent.RemoveListener(React);
	}

	private void React(bool obj)
	{
		if(obj)
		{
			StartChecking();
		}
		else
		{
			StopAllCoroutines();
		}
	}

	private void StartChecking()
	{
		StartCoroutine(CheckingForWin());
	}

	private IEnumerator CheckingForWin()
	{
		while(true)
		{
			if(WinningCondition())
			{
				StoppedEvent.RaiseEvent(true);
				StopAllCoroutines();
				BrickAmount.Add(LevelBricks.Count * WinMultiplier);
			}
			yield return new WaitForSecondsRealtime(0.2f);
		}
	}

	private bool WinningCondition()
	{
		int nrOfMatches = 0;
		List<Brick> playerBricks = FindAllPlayerBricks();
		if (playerBricks.Count != LevelBricks.Count) return false;
		LevelBricks.ForEach(levelBrick =>
		{
			playerBricks.ForEach(playerBrick =>
			{
				if (BricksMatch(playerBrick, levelBrick)) nrOfMatches++;
			});
		});
		return nrOfMatches == LevelBricks.Count;
	}

	private bool BricksMatch(Brick a, Brick b)
	{
		if (Vector3.Distance(a.transform.position, b.transform.position) > 0.01f) return false;
		if (!RotationsMatch(a.transform.rotation, b.transform.rotation)) return false;
		if (a.BrickColor != b.BrickColor) return false;
		return true;
	}

	private bool RotationsMatch(Quaternion a, Quaternion b)
	{
		Quaternion bFlipped = b * Quaternion.AngleAxis(180f, Vector3.up);
		
		return Quaternion.Angle(a, b) < 0.01f || Quaternion.Angle(a, bFlipped) < 0.01f;
	}

	private List<Brick> FindAllPlayerBricks()
	{
		List<Brick> playerBricks = new List<Brick>();
		
		Collider[] foundColliders = Physics.OverlapBox(PlayVolume.transform.position, PlayVolume.size * 0.5f, PlayVolume.transform.rotation,
			PlayerBricksLayerMask);
		Debug.Log("Found " + foundColliders.Length + " bricks(with noncollectables)");
		foundColliders.ForEach(coll =>
		{
			Brick b = coll.GetComponent<Brick>();
			if (b != null && b.Collectable)
			{
				playerBricks.Add(b);
			}
		});
		Debug.Log("Found " + playerBricks.Count + " bricks");
		return playerBricks;
	}
}
