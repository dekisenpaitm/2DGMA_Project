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
    #region Movement
    [Header("Movement_Speed")]
    [SerializeField]
    private float playerSpeed;
    private float _playersMovementDirection = 0;
    private bool isMovementStopped = false;
    #endregion

    #region Jump
    [Header("Jump_Force")]
    [SerializeField]
    private float playerJumpForce;
    [Header("Jump_Count")]
    [SerializeField]
    private int playerJumpCount = 2;
    #endregion

    #region MovementBools
    [Header("Jump_Force")]
    [SerializeField]
    private bool _isFlipped;
    [SerializeField]
    private bool dashing;
    [SerializeField]
    private bool jumping;
    #endregion

    #region Referances
    public PlayerStates currentState;
    private SpineAnimationController _spineAnim;
    private PlayerControlls _inputActionReference;
    private Rigidbody2D _playersRigidBody;
    private Animator _anim;
    public ParticleSystem dust;
    public ParticleSystem dashTrail;
    public SmoothCameraFollow cam;
    #endregion



    private void Start()
    {
        cam = FindObjectOfType<SmoothCameraFollow>();
        _anim = GetComponent<Animator>();
        _spineAnim = FindObjectOfType<SpineAnimationController>(includeInactive: true);
        currentState = PlayerStates.idle;
        _playersRigidBody ??= GetComponent<Rigidbody2D>();
        _inputActionReference = new PlayerControlls();
        _inputActionReference.Enable();
        _inputActionReference.Movement.Move.performed += moving =>
        {
                _playersMovementDirection = moving.ReadValue<float>();
        };
        _inputActionReference.Movement.Jump.performed += jumping => { JumpThePlayer(); };
        _inputActionReference.Movement.Dash.performed += dashing => { Dash(); };
        _inputActionReference.Movement.Dash.performed += attacking => { Attack(); };
    }


    private void Update()
    {

        if (!_isFlipped && _playersMovementDirection < 0)
        {
            _spineAnim.FlipSprite();
            _isFlipped = true;
            if (playerJumpCount >= 2)
            {
                dust.Play();
            }
        }

        if(_playersMovementDirection > 0 && _isFlipped)
        {
            _isFlipped = false;
            _spineAnim.FlipSprite();
            if (playerJumpCount >= 2)
            {
                dust.Play();
            }
        }



        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float wiggleRoom = 10;
        
        if (!isMovementStopped && screenPos.x >= wiggleRoom)
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
        } else
        {
            _playersRigidBody.velocity = Vector2.zero;
            transform.position = new Vector2(transform.position.x + 0.1f, transform.position.y);
        }
    }

    private void Dash()
    {
        if (!jumping)
        {
            StartCoroutine(PlayerDash());
        }
    }

    private void Attack()
    {
        //implement Attack here
    }

    private IEnumerator PlayerDash()
    {
        dashTrail.Play();
        if (playerJumpCount >= 2)
        {
            dust.Play();
        }
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
            if(playerJumpCount >= 2)
            {
                dust.Play();
            }
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

        if (collision.gameObject.CompareTag("Floor"))
        {
            if (contactPoint.y > ownCenter.y + ownBounds.extents.y * 0.5)
            {
                //Debug.LogError("HIT MY HEAD");
                isMovementStopped = true;
            }
            Debug.Log(isMovementStopped);

            if (contactPoint.y > ownCenter.y)
            {
                if (playerJumpCount < 2 && contactPoint.y > ownCenter.y - ownBounds.extents.y * 0.5)
                {
                    _anim.Play("Player_Land");
                }
                playerJumpCount = 2;
                isMovementStopped = false;
            }
        }
    }
}
