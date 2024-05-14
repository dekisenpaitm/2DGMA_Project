using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target == null)
        {
            target = FindObjectOfType<PlayerController>().gameObject.transform;
        }
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
