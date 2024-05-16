using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private bool _cameraChanged;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(target.position.y > 5 && !_cameraChanged)
        {
            _cameraChanged = true;
            offset = new Vector3(offset.x, 0, offset.z);
        } else if(target.position.y < 5 )
        {
            _cameraChanged = false;
            offset = new Vector3(offset.x, 5, offset.z);
        }

        if(target == null)
        {
            target = FindObjectOfType<PlayerController>().gameObject.transform;
        }
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
