using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("Object_To_Follow")]
    public Transform target;

    [Header("Cam_Attributes")]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float minY;
    public float maxY;
    public bool cameraHitBoundary;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            target = FindObjectOfType<PlayerController>().gameObject.transform;
        }
        Vector3 desiredPosition = target.position + offset;
        if (desiredPosition.x > transform.position.x)
        {
            ApplyMove(desiredPosition);
        } else
        {
            RestrictMovement(desiredPosition);
        }
        
    }

    public void ApplyMove(Vector3 desiredPosition)
    {
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        smoothPosition.y = checkForYBoundary(smoothPosition.y);
        transform.position = smoothPosition;
    }

    public void RestrictMovement(Vector3 desiredPosition)
    {
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, new Vector3 (transform.position.x,desiredPosition.y, desiredPosition.z), ref velocity, smoothSpeed);
        smoothPosition.y = checkForYBoundary(smoothPosition.y);
        transform.position = smoothPosition;
    }
    

    private float checkForYBoundary(float proposedY)
    {
        return Mathf.Clamp(proposedY, minY, maxY);
    }
}

