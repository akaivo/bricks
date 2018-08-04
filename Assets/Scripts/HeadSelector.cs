using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSelector : Photon.MonoBehaviour
{
    private void Start()
    {
        List<Transform> heads = new List<Transform>();
        foreach (Transform child in transform)
        {
            heads.Add(child);
            child.gameObject.SetActive(false);
        }

        if (photonView.isMine) return;//don't show own head
        
        int chosenIndex = Random.Range(0, heads.Count);
        heads[chosenIndex].gameObject.SetActive(true);
    }

}