using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBrick : MonoBehaviour
{

	public Snap snapper;

	private SteamVR_TrackedController _controller;

	private void OnEnable()
	{
		_controller = GetComponent<SteamVR_TrackedController>();
		_controller.TriggerClicked += Place;
	}

	private void OnDisable()
	{
		_controller.TriggerClicked -= Place;
	}

	private void Place(object sender, ClickedEventArgs e)
	{
		if(snapper.IsSnapped)
		{
			Instantiate(snapper.SnappingBrick.gameObject).layer = LayerMask.NameToLayer("Bricks");
		}
	}
}
