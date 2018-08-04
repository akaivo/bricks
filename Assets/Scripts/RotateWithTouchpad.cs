using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Vector3 = UnityEngine.Vector3;

public class RotateWithTouchpad : MonoBehaviour
{
    public GameObject ControllerGO;

    private Vector3 globalHeading = Vector3.left;

    private SteamVR_Controller.Device device
    {
        get { return SteamVR_Controller.Input((int) ControllerGO.GetComponent<SteamVR_TrackedObject>().index); }
    }

    private readonly EVRButtonId _touchPad = EVRButtonId.k_EButton_SteamVR_Touchpad;
    private readonly float _sensitivity = 3f;

    private void Update()
    {
        if (device.GetTouch(_touchPad))
        {
            globalHeading = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(device.GetAxis(_touchPad).x * _sensitivity, Vector3.up), Vector3.one)
                .MultiplyVector(globalHeading);
            /*var h = device.GetAxis(touchPad);
            globalHeading = ControllerGO.transform.TransformDirection(new Vector3(-h.x, h.y, 0f));
            globalHeading = Vector3.ProjectOnPlane(globalHeading, Vector3.up);*/
        }

        transform.rotation = Quaternion.LookRotation(globalHeading);
    }
}