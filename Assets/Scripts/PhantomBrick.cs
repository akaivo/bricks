using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomBrick : MonoBehaviour
{
    public GameObject PhantomBrickPrefab;

    private Transform _phantomBrick;

    private void Start()
    {
        _phantomBrick = Instantiate(PhantomBrickPrefab).transform;
    }

    private void Update()
    {
        _phantomBrick.position = transform.position;
        _phantomBrick.rotation = transform.rotation;
    }
}