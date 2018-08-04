using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedRightHand : NetworkedHand
{
    public Snap Snapper;
    
    public List<GameObject> AvailabelBricksPrefabs;
    private List<Brick> _bricks = new List<Brick>();
    private Brick _activeBrick;
    private int _currentBrickIndex = 0;

    private void Start()
    {
        for (int i = 0; i < AvailabelBricksPrefabs.Count; i++)
        {
            _bricks.Add(Instantiate(AvailabelBricksPrefabs[i]).GetComponent<Brick>());
            _bricks[i].gameObject.SetActive(i == _currentBrickIndex);
        }

        _activeBrick = _bricks[_currentBrickIndex];
    }

    private void Update()
    {
        Snapper.TrySnapping(_activeBrick);
    }

    protected override void HandlePadClick(object sender, ClickedEventArgs e)
    {
        int newIndex = _currentBrickIndex + 1;
        if (newIndex >= _bricks.Count)
        {
            newIndex = 0;
        }

        photonView.RPC("ReceiveIndexChangeClick", PhotonTargets.AllBufferedViaServer, newIndex);
    }

    [PunRPC]
    private void ReceiveIndexChangeClick(int newIndex)
    {
        _bricks[_currentBrickIndex].gameObject.SetActive(false);
        _bricks[newIndex].gameObject.SetActive(true);
        _currentBrickIndex = newIndex;
        _activeBrick = _bricks[newIndex];
    }

    protected override void HandleTriggerClick(object sender, ClickedEventArgs e)
    {
        if (Snapper.TrySnapping(_activeBrick))
        {
            GameObject go = PhotonNetwork.Instantiate(AvailabelBricksPrefabs[_currentBrickIndex].name,
                _activeBrick.transform.position,
                _activeBrick.transform.rotation, 0);
            go.GetComponent<Brick>().SetLayer(LayerMask.NameToLayer("Bricks"));
            PulseController();
        }
    }

    [PunRPC]
    private void ReceiveTriggerClick(Vector3 pos, Quaternion rot)
    {
        Instantiate(_activeBrick.gameObject, pos, rot).layer = LayerMask.NameToLayer("Bricks");
    }
}