using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Blade : MonoBehaviour
{
    [Header("Object_To_Follow")]
    public PlayerController _player;

    [Header("Offset")]
    public float offset_x;
    public float offset_y;

    [Header("Smooth_Speed")]
    public float smoothSpeed;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, new Vector2(_player.transform.position.x - offset_x, _player.transform.position.y + offset_y), ref velocity, smoothSpeed);
        transform.position = smoothPosition;
    }
}
