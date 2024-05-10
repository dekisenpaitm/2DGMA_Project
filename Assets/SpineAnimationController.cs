using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class shows off the two most basic Spine commands 'SetAnimation()' and 'AddAnimation()'
/// as well as showing the usage of coroutines
/// </summary>

public class SpineAnimationController : MonoBehaviour
{
    // [SpineAnimation] creates a dropdown in the inspector that let's us choose from all the animations on a specific 'SkeletonAnimation' instance
    [SpineAnimation]
    public string dashAnimation;

    [SpineAnimation]
    public string runAnimation;

    [SpineAnimation]
    public string idleAnimation;

    public Animator _playerAnim;

    private TrackEntry _spineAnim;

    private PlayerController _player;
    private PlayerStates current;



    // Spine component that lets us manipulate the animations and skeleton of our spine character
    SkeletonAnimation skeletonAnimation;

    // 'AnimationState' keeps track of which animation the character is currently in/playing
    Spine.AnimationState animationState;
    Skeleton skeleton;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>(includeInactive: true);
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        animationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;
        FlipSprite();
    }

    /// <summary>
    /// A coroutine allows you to spread tasks across several frames.
    /// In Unity, a coroutine is a method that can pause execution and return control to Unity but then continue where it left off on the following frame.
    /// 
    /// For more info see: https://docs.unity3d.com/Manual/Coroutines.html
    /// </summary>


    public void Update()
    {
        switch (_player.currentState)
        {
            case PlayerStates.idle:
                if (_player.currentState != current)
                {
                    current = PlayerStates.idle;
                    _spineAnim = animationState.SetAnimation(0, idleAnimation, true);
                }
                break;
            case PlayerStates.jumping:
                if (_player.currentState != current)
                {
                    current = PlayerStates.jumping;
                    _spineAnim = animationState.SetAnimation(0, runAnimation, true);
                }
                break;
            case PlayerStates.running:
                if (_player.currentState != current)
                {
                    current = PlayerStates.running;
                    _spineAnim = animationState.SetAnimation(0, runAnimation, true);
                }
                break;
            case PlayerStates.dashing:
                if (_player.currentState != current)
                {
                    current = PlayerStates.dashing;
                    _spineAnim = animationState.SetAnimation(0, dashAnimation, false);
                }
                break;
        }
    }

    public void FlipSprite()
    {
        skeleton.ScaleX *= -1;
    }
}