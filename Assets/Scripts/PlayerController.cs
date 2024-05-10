using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum PlayerStates
{
    idle,
    running,
    dashing,
    jumping
}

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    private float _playersMovementDirection = 0;
    private PlayerControlls _inputActionReference;
    private Rigidbody2D _playersRigidBody; 
    public float playerJumpForce; 
    public int playerJumpCount = 2;
    private bool isMovementStopped = false;
    public PlayerStates currentState;
    private SpineAnimationController _spineAnim;
    private bool _isFlipped;
    private bool dashing;
    private bool jumping;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _spineAnim = FindObjectOfType<SpineAnimationController>(includeInactive: true);
        currentState = PlayerStates.idle;
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
        _inputActionReference.Movement.Dash.performed += dashing => { Dash(); };
    }


    private void Update()
    {
        if (!_isFlipped && _playersMovementDirection < 0)
        {
            _spineAnim.FlipSprite();
            _isFlipped = true;
        }

        if(_playersMovementDirection > 0 && _isFlipped)
        {
            _isFlipped = false;
            _spineAnim.FlipSprite();
        }

        if (!isMovementStopped)
        {
            //Moving player using player rigid body.
            _playersRigidBody.velocity =
                    new Vector2(_playersMovementDirection * playerSpeed, _playersRigidBody.velocity.y);
            if((_playersRigidBody.velocity.y != 0 || _playersRigidBody.velocity.x != 0) && !dashing)
            {
                Debug.Log(_playersMovementDirection);
                currentState = PlayerStates.running;
            } else if ((_playersRigidBody.velocity.y == 0 && _playersRigidBody.velocity.x == 0) && currentState != PlayerStates.jumping && !dashing)
            {
                currentState = PlayerStates.idle;
            }
        }
    }

    private void Dash()
    {
        if (!jumping)
        {
            StartCoroutine(PlayerDash());
        }
    }

    private IEnumerator PlayerDash()
    {
        dashing = true;
        playerSpeed = 10;
        currentState = PlayerStates.dashing;
        _playersRigidBody.gravityScale = 1;
        yield return new WaitForSeconds(0.5f);
        currentState = PlayerStates.idle;
        _playersRigidBody.gravityScale = 3;
        playerSpeed = 5;
        dashing = false;
    }

    private void JumpThePlayer()
    {
        if (playerJumpCount > 0 && !dashing)
        {
            jumping = true;
            _anim.Play("Player_Jump"); 
        }
    }

    public void Jump()
    {
        
        currentState = PlayerStates.jumping;
        playerJumpCount--;
        //Moving player using player rigid body.
        _playersRigidBody.velocity = Vector2.up * playerJumpForce;
        jumping = false;
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
