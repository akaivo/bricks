using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndMove : MonoBehaviour
{
    public GameObject prefab;
    public SteamVR_TrackedController Controller;
    private Transform networkedObject;

    public void OnJoinedRoom()
    {
        networkedObject = PhotonNetwork.Instantiate(prefab.name, transform.position, transform.rotation, 0).transform;
        NetworkedHand hand = networkedObject.GetComponent<NetworkedHand>();
        if (hand != null)
        {
            hand.SetController(Controller);
        }
    }

    private void Update()
    {
        if (networkedObject)
        {
            networkedObject.position = transform.position;
            networkedObject.rotation = transform.rotation;
            networkedObject.localScale = transform.lossyScale;
        }
    }
}