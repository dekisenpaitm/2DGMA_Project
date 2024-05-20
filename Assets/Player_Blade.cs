using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Blade : MonoBehaviour
{
    public PlayerController _player;
    public float offset_x;
    public float offset_y;
    public float smoothSpeed;
    private Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
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
