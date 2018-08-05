using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RemoveBricksOnWin : MonoBehaviour
{

	public BooleanEvent WinEvent;
	public BoxCollider PlayVolume;
	public LayerMask PlayerBricksLayerMask;


	private void Awake()
	{
		WinEvent.AddListener(ClearPlayArea);
	}

	private void OnDestroy()
	{
		WinEvent.RemoveListener(ClearPlayArea);
	}

	private void ClearPlayArea(bool win)
	{
		if (!win) return;
		foreach (Brick playerBrick in FindAllPlayerBricks())
		{
			playerBrick.GetComponent<PhotonView>().RequestOwnership();
			PhotonNetwork.Destroy(playerBrick.gameObject);
		}
	}

	private List<Brick> FindAllPlayerBricks()
	{
		List<Brick> playerBricks = new List<Brick>();
		
		Collider[] foundColliders = Physics.OverlapBox(PlayVolume.transform.position, PlayVolume.size * 0.5f, PlayVolume.transform.rotation,
			PlayerBricksLayerMask);
		Debug.Log("Found " + foundColliders.Length + " bricks(with noncollectables)");
		foundColliders.ForEach(coll =>
		{
			Brick b = coll.GetComponent<Brick>();
			if (b != null && b.Collectable)
			{
				playerBricks.Add(b);
			}
		});
		Debug.Log("Found " + playerBricks.Count + " bricks");
		return playerBricks;
	}

}
