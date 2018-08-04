using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public abstract class NetworkedHand : Photon.MonoBehaviour
{
	private SteamVR_TrackedController _controller;
	protected SteamVR_Controller.Device Device { get { return SteamVR_Controller.Input((int)_controller.controllerIndex); } }
	protected readonly EVRButtonId _trigger = EVRButtonId.k_EButton_SteamVR_Trigger;

	public void SetController(SteamVR_TrackedController c)
	{
		_controller = c;
		_controller.TriggerClicked += HandleTriggerClick;
		_controller.PadClicked += HandlePadClick;
	}

	protected abstract void HandlePadClick(object sender, ClickedEventArgs e);

	protected abstract void HandleTriggerClick(object sender, ClickedEventArgs e);
	
	protected void PulseController()
	{
		Device.TriggerHapticPulse(1000);
	}
}
