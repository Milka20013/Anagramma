using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public SpriteRenderer outline;

    void Start() // align camera to screensize
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = outline.bounds.size.x / outline.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = outline.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = outline.bounds.size.y / 2 * differenceInSize;
        }
    }
}