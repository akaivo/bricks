using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndMove : MonoBehaviour
{

	public GameObject handPrefab;
	private Transform networkedHand;
	
	public void OnJoinedRoom()
	{
		Debug.Log("Spawn hand");
		networkedHand = PhotonNetwork.Instantiate(handPrefab.name, transform.position, transform.rotation, 0).transform;
		networkedHand.GetComponent<NetworkedHand>().SetController(GetComponent<SteamVR_TrackedController>());
	}

	private void Update()
	{
		if(networkedHand)
		{
			networkedHand.position = transform.position;
			networkedHand.rotation = transform.rotation;
			networkedHand.localScale = transform.lossyScale;
		}
	}
}
