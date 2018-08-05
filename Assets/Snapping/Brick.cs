using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : Photon.MonoBehaviour
{
	public BrickColorEnum BrickColor;
	public bool Collectable = true;
	public BrickType BrickType;
	public List<SnapPositions> MySnapPositions;
	
	public void SetLayer(int layer)
	{
		photonView.RPC("ReceiveLayer", PhotonTargets.AllBufferedViaServer, layer);
	}
	
	[PunRPC]
	private void ReceiveLayer(int layer)
	{
		gameObject.layer = layer;
	}
}
