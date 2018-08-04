using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NetworkedHand : MonoBehaviour
{

	private SteamVR_TrackedController _controller;

	public void SetController(SteamVR_TrackedController c)
	{
		_controller = c;
		_controller.TriggerClicked += HandleTriggerClick;
	}

	protected abstract void HandleTriggerClick(object sender, ClickedEventArgs e);
}
