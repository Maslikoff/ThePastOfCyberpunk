using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaOneShotBehavior : StateMachineBehaviour
{
    public AudioClip soundToPlay;
    public float volume = 1f;
    public float playDelay = .25f;
    public bool playOnEnter = true, playOnExit = false, playAfterDelay = false;

    private float timeSineceEntered = 0;
    private bool hasDelayedSoundPlayed = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnEnter)
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);

        timeSineceEntered = 0f;
        hasDelayedSoundPlayed = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playAfterDelay && !hasDelayedSoundPlayed)
        {
            timeSineceEntered += Time.deltaTime;

            if (timeSineceEntered > playDelay)
            {
                AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
                hasDelayedSoundPlayed = true;
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playOnExit)
            AudioSource.PlayClipAtPoint(soundToPlay, animator.gameObject.transform.position, volume);
    }
}
