using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    public Transform originPoint;

    public void onRotation(float value)
    {
        Vector3 newRotation = originPoint.eulerAngles;
        newRotation.y = value;
        originPoint.eulerAngles = newRotation;
    }
    public void onTransform(float value)
    {
        Vector3 newPosition = originPoint.position;
        newPosition.y = -value + 15f;
        originPoint.position = newPosition;
    }

}
