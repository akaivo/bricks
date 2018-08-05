using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScore : MonoBehaviour
{

	public TextMesh TextMesh;
	
	private void Awake()
	{
		BrickAmount.Changed += UpdateScore;
	}

	private void UpdateScore(int amount)
	{
		TextMesh.text = "Bricks: " + amount;
	}

	private void OnDestroy()
	{
		BrickAmount.Changed -= UpdateScore;
	}
}
