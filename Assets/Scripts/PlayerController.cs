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


    private void FixedUpdate()
    {
        //Moving player using player rigid body.
        _playersRigidBody.velocity =
            new Vector2(_playersMovementDirection * playerSpeed, _playersRigidBody.velocity.y);
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
        var contact = collision.GetContact(0);
        var contactPoint = contact.point;
        var ownCenter = contact.collider.bounds.center;

        if (collision.gameObject.CompareTag("Floor") && contactPoint.y > ownCenter.y)
        {
            playerJumpCount = 2;
        }
    }

}
