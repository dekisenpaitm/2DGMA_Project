using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class shows off the two most basic Spine commands 'SetAnimation()' and 'AddAnimation()'
/// as well as showing the usage of coroutines
/// </summary>

public class SpineBasicCommands : MonoBehaviour
{
    // [SpineAnimation] creates a dropdown in the inspector that let's us choose from all the animations on a specific 'SkeletonAnimation' instance
    [SpineAnimation]
    public string dashAnimation;

    [SpineAnimation]
    public string runAnimation;

    [SpineAnimation]
    public string idleAnimation;

    // Spine component that lets us manipulate the animations and skeleton of our spine character
    SkeletonAnimation skeletonAnimation;

    // 'AnimationState' keeps track of which animation the character is currently in/playing
    Spine.AnimationState animationState;
    Spine.Skeleton skeleton;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        animationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.skeleton;

        // start the coroutine
        StartCoroutine(Animation());
    }

    /// <summary>
    /// A coroutine allows you to spread tasks across several frames.
    /// In Unity, a coroutine is a method that can pause execution and return control to Unity but then continue where it left off on the following frame.
    /// 
    /// For more info see: https://docs.unity3d.com/Manual/Coroutines.html
    /// </summary>

    IEnumerator Animation()
    {
        while (true)
        {
            // 'SetAnimation' is used to immediatelly switch out any current animations for a new one
            TrackEntry entry = animationState.SetAnimation(0, runAnimation, true);
            // used to wait a specified amount of seconds inside a coroutine
            yield return new WaitForSeconds(2f);

            animationState.SetAnimation(0, runAnimation, true);
            yield return new WaitForSeconds(1.5f);

            animationState.SetAnimation(0, idleAnimation, true);
            yield return new WaitForSeconds(1.5f);

            // flipping the skeleton on the x-Axis means flipping the character
            skeleton.ScaleX *= -1;
            //animationState.SetAnimation(0, turnAnimation, false);
            // 'AddAnimation' adds the animation into a queue, it gets played as soon as the animation before is finished
            animationState.AddAnimation(0, idleAnimation, true, 0);
            yield return new WaitForSeconds(1f);
        }
    }
}