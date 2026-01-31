using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraLerp : MonoBehaviour
{
    public Transform cam;
    public Vector3 offset = new();
    public float smoothTime = 0.2f;
    private Vector3 velT = new();

    void Start()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 desiredPos = cam.position + offset;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPos,
            ref velT,
            smoothTime
        );
    }
}
