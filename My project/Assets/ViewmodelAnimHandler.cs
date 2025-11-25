using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enigma;

public class ViewmodelAnimHandler : MonoBehaviour
{
    Animator animator;
    public PhysicsEntity entity;
    string state;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(entity.GetState() != state)
        {
            state = entity.GetState();
            animator.CrossFadeInFixedTime(state,.25f);
        }
        animator.SetFloat("Velocity",entity.body.linearVelocity.magnitude);
        animator.SetFloat("Grounded",entity.grounded? 1 : 0);
        float facing = Vector3.Dot(transform.forward, entity.body.transform.forward);
        Debug.Log(facing);
        //animator.SetFloat("Facing",)
    }
}
