using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class NetworkedLeftHand : NetworkedHand
{

	public BrickFinder BrickFinder;
	
	protected override void HandlePadClick(object sender, ClickedEventArgs e)
	{
	}

	protected override void HandleTriggerClick(object sender, ClickedEventArgs e)
	{
	}

	private void Update()
	{
		if (Device.GetPress(_trigger))
		{
			BrickFinder.BricksInCapsule.ForEach(Collect);
			BrickFinder.BricksInCapsule.Clear();
		}
	}

	private void Collect(Brick brick)
	{
		PhotonNetwork.Destroy(brick.gameObject);
		PulseController();
	}

	
}
