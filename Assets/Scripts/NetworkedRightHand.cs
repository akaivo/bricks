using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedRightHand : NetworkedHand
{

	public Snap Snapper;
	
	protected override void HandleTriggerClick(object sender, ClickedEventArgs e)
	{
		if(Snapper.IsSnapped)
		{
			Instantiate(Snapper.SnappingBrick.gameObject).layer = LayerMask.NameToLayer("Bricks");
		}
	}
}
