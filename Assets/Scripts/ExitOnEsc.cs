using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOnEsc : MonoBehaviour {

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
	}

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
