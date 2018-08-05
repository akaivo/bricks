using UnityEngine;
using System.Collections;
//using Valve.VR;
using System;
using Valve.VR;

/* Aadam Kaivo */
public class PinchNavigation : MonoBehaviour
{
    public GameObject rightGO;
    public GameObject leftGO;
    public float inertialStopSpeed = 0.1f;
    public bool ScalingEnabled = true;
    private SteamVR_Controller.Device right { get { return SteamVR_Controller.Input((int)rightGO.GetComponent<SteamVR_TrackedObject>().index); } }
    private SteamVR_Controller.Device left { get { return SteamVR_Controller.Input((int)leftGO.GetComponent<SteamVR_TrackedObject>().index); } }

    private EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;
    private EVRButtonId grip = EVRButtonId.k_EButton_Grip;

    private Vector3 vectorFromLeftToRight { get { return rightSmoothedPosition - leftSmoothedPosition; } }

    //position
    private Vector3 lastFramePosition = Vector3.zero;
    private Vector3 thisFramePosition;
    private Vector3 moveDelta = Vector3.zero;

    //rotation
    private Vector3 from;
    private Vector3 to;

    //scale
    private Vector3 initialScale;
    private float fromScale;
    private float toScale;

    //smoothing
    public int avgParam = 4;//seems to be good balance between smoothing and delay when using simple moving average;
    private Vector3 rightSmoothedPosition;
    private Vector3 leftSmoothedPosition;
    private Queue rightPositions = new Queue(4);
    private Queue leftPositions = new Queue(4);
    
    void Update()
    {
        rightPositions.Enqueue(rightGO.transform.localPosition);
        leftPositions.Enqueue(leftGO.transform.localPosition);
        while (rightPositions.Count > avgParam) rightPositions.Dequeue();
        while (leftPositions.Count > avgParam) leftPositions.Dequeue();
        rightSmoothedPosition = GetMeanVector(rightPositions);
        leftSmoothedPosition = GetMeanVector(leftPositions);

        if (right.GetPress(grip) && left.GetPress(grip))
        {
            if (right.GetPressDown(grip) || left.GetPressDown(grip))
            {
                //position
                lastFramePosition = rightGO.transform.position;

                //rotation
                from = vectorFromLeftToRight;
                from.y = 0f;

                //scale
                if(ScalingEnabled)
                {
                    initialScale = transform.localScale;
                    fromScale = vectorFromLeftToRight.magnitude;
                }
            }

            //position
            thisFramePosition = rightGO.transform.position;
            moveDelta = (lastFramePosition - thisFramePosition);
            transform.position = transform.position + moveDelta;
            lastFramePosition = thisFramePosition + moveDelta;

            //rotation
            to = vectorFromLeftToRight;
            to.y = 0f;
            float angleDelta = (Vector3.Cross(from, to).y < 0) ? Vector3.Angle(from, to) : -Vector3.Angle(from, to);
            transform.RotateAround(leftGO.transform.position, Vector3.up, angleDelta);
            from = to;

            //scale
            if (ScalingEnabled)
            {
                toScale = vectorFromLeftToRight.magnitude;
                transform.localScale = initialScale * (fromScale / toScale);
                float clamped = Mathf.Clamp(transform.localScale.x, 0.5f, 100f);
                transform.localScale = Vector3.one * clamped;
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
        if (right.GetPress(grip) && !left.GetPress(grip))
        {
            if (right.GetPressDown(grip))
            {
                //position
                lastFramePosition = rightGO.transform.position;
            }

            //position
            thisFramePosition = rightGO.transform.position;
            moveDelta = (lastFramePosition - thisFramePosition);
            transform.position = transform.position + moveDelta;
            lastFramePosition = thisFramePosition + moveDelta;
        }
        if (right.GetPressUp(grip) && left.GetPress(grip))
        {
            //position
            lastFramePosition = leftGO.transform.position;
        }
        if (left.GetPress(grip) && !right.GetPress(grip))
        {
            if (left.GetPressDown(grip))
            {
                //position
                lastFramePosition = leftGO.transform.position;
            }

            //position
            thisFramePosition = leftGO.transform.position;
            moveDelta = (lastFramePosition - thisFramePosition);
            transform.position = transform.position + moveDelta;
            lastFramePosition = thisFramePosition + moveDelta;
        }

        //inertial movement
        float currentUnscaledSpeed = moveDelta.magnitude / (transform.localScale.magnitude * Time.unscaledDeltaTime);
        if (currentUnscaledSpeed < inertialStopSpeed) moveDelta = Vector3.zero;

        if (!left.GetPress(grip) && !right.GetPress(grip))
        {
            if(currentUnscaledSpeed != 0)
            {
                moveDelta = moveDelta * (1 - 2 * Time.unscaledDeltaTime);
                transform.position = transform.position + moveDelta;
            }
        }
    }

    private Vector3 GetMeanVector(Queue positions)
    {
        if (positions.Count == 0)
            return Vector3.zero;
        float x = 0f;
        float y = 0f;
        float z = 0f;
        foreach (Vector3 pos in positions)
        {
            x += pos.x;
            y += pos.y;
            z += pos.z;
        }
        return new Vector3(x / positions.Count, y / positions.Count, z / positions.Count);
    }
}

