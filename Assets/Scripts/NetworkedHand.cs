using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NetworkedHand : Photon.MonoBehaviour
{

	private SteamVR_TrackedController _controller;

	public void SetController(SteamVR_TrackedController c)
	{
		_controller = c;
		_controller.TriggerClicked += HandleTriggerClick;
		_controller.PadClicked += HandlePadClick;
	}

	protected abstract void HandlePadClick(object sender, ClickedEventArgs e);

	protected abstract void HandleTriggerClick(object sender, ClickedEventArgs e);
}
