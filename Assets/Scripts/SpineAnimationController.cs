using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineAnimationController : MonoBehaviour {

    #region Spine_Animations
    [SpineAnimation]
    public string dashAnimation;

    [SpineAnimation]
    public string runAnimation;

    [SpineAnimation]
    public string idleAnimation;
    #endregion

    #region Refs
    public Animator _playerAnim;
    private TrackEntry _spineAnim;
    private PlayerController _player;
    private FlipEffectController _flipEffect;
    private PlayerStates current;
    private SkeletonAnimation skeletonAnimation;
    private Spine.AnimationState animationState;
    private Skeleton skeleton;
    #endregion

    void Start()
    {
        _flipEffect = FindObjectOfType<FlipEffectController>(includeInactive: true);
        _player = FindObjectOfType<PlayerController>(includeInactive: true);
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        animationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;
        FlipSprite();
    }

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
        _flipEffect.FlipEffects();
        skeleton.ScaleX *= -1;
    }
}