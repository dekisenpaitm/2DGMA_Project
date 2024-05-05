using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private float _playersMovementDirection = 0;
    private PlayerControlls _inputActionReference;
    private Rigidbody2D _playersRigidBody; 
    public float playerJumpForce; 
    public int playerJumpCount = 2;
    private bool isMovementStopped = false;

    private void Start()
    {
        //Getting the reference of the players rigid body.
        _playersRigidBody ??= GetComponent<Rigidbody2D>();

        _inputActionReference = new PlayerControlls();
        //enabling the Input actions
        _inputActionReference.Enable();
        //reading the values of the player movement direction for the players movement.
        _inputActionReference.Movement.Move.performed += moving =>
        {

                _playersMovementDirection = moving.ReadValue<float>();
 
        };

        _inputActionReference.Movement.Jump.performed += jumping => { JumpThePlayer(); };
    }


    private void Update()
    {
        if (!isMovementStopped)
        {
            //Moving player using player rigid body.
            _playersRigidBody.velocity =
                    new Vector2(_playersMovementDirection * playerSpeed, _playersRigidBody.velocity.y);
        }
    }

    private void JumpThePlayer()
    {
        if (playerJumpCount > 0)
        {
            playerJumpCount--;
            //Moving player using player rigid body.
            _playersRigidBody.velocity = Vector2.up * playerJumpForce;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        var contact = collision.GetContact(0);
        var contactPoint = contact.point;
        var ownCenter = contact.collider.bounds.center;
        var ownBounds = contact.collider.bounds;

        // Reset movement stop when hitting the floor
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Check if the contact point is significantly above the center, indicating a non-leg hit
            if (contactPoint.y > ownCenter.y + ownBounds.extents.y * 0.5)
            {
                Debug.LogError("HIT MY HEAD");
                isMovementStopped = true;  // Stop movement if hit with an upper part of the bod
            }
            Debug.Log(isMovementStopped);
            // Check if the contact point is at the top part of the collider (assuming legs are at the bottom)
            if (contactPoint.y > ownCenter.y)
            {
                playerJumpCount = 2;  // Reset jump count if hit with legs
                isMovementStopped = false;
            }
        }
    }

}
