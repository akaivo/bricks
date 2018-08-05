using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWin : MonoBehaviour
{
    public GameObject WinShowObject;
    public ParticleSystem ParticleSystem;
    public BooleanEvent WinEvent;

    private void Awake()
    {
        WinEvent.AddListener(ShowWinObject);
    }

    private void OnDestroy()
    {
        WinEvent.RemoveListener(ShowWinObject);
    }

    private void ShowWinObject(bool value)
    {
        WinShowObject.SetActive(value);
        if(value) ParticleSystem.Play();
        StartCoroutine(WaitAndHide());
    }

    private IEnumerator WaitAndHide()
    {
        yield return new WaitForSecondsRealtime(3f);
        WinShowObject.SetActive(false);
    }
}