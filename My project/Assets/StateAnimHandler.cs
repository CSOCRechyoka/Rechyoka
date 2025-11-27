using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enigma;

public class StateAnimHandler : MonoBehaviour
{
    public Animator animator;
    public PhysicsEntity entity;
    bool animLock = false;
    string state;
    // Start is called before the first frame update
    void Start()
    {
        if(!animator)
            animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(entity.GetState() != state)
        // {
        //     state = entity.GetState();
        //     animator.CrossFadeInFixedTime(state,.25f);
        // }
        // animator.SetFloat("Velocity",entity.body.linearVelocity.magnitude);
        // animator.SetFloat("Grounded",entity.grounded? 1 : 0);
        // float facing = Vector3.Dot(transform.forward, entity.body.transform.forward);
        // Debug.Log(facing);
        // //animator.SetFloat("Facing",)
    }

    void Play(string state, float transitionTime)
    {
        //StateAnimHandler.Play("Telekinesis", "Grab")
    }

    IEnumerator Transition(string toState, string fromState, float transitionLength = 0, int layerIndex = -1)
    {
        if(animLock == false)
        {
            if(fromState == null)
            {
                animator.CrossFadeInFixedTime(toState,transitionLength,layerIndex);
                yield return null;
            }
            else
            {
                animator.CrossFadeInFixedTime(fromState,layerIndex);
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
                float transitionTime = transitionLength/stateInfo.length; // percentage of animation
                yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime <= transitionTime);
                if(stateInfo.fullPathHash == animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash);
                animator.CrossFadeInFixedTime(toState, transitionLength);
            }
        }
    }
}
