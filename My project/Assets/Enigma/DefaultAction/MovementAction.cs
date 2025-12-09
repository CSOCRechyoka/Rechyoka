using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enigma
{
    public class MovementAction : Action
    {
        PhysicsEntity entity;
        [Header("Inputs")]
        public ActionInput horizontal;
        public ActionInput vertical;
        public ActionInput sprint;
        [Header("Variables")]
        public float walkSpeed;
        public float walkAcceleration;
        [Space(5)]
        public float sprintSpeed;
        public float sprintAcceleration;
        [Space(5)]
        public float deceleration;
        public float turnSpeed;
        [Space(10)]
        public TargetAxis moveAxis;
        //
        Vector3 forwardDir;
        float activeMoveSpeed;

        void Start()
        {
            entity = GetComponentInChildren<PhysicsEntity>();
            forwardDir = transform.forward;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float hor = horizontal.GetInput();
            float ver = vertical.GetInput();
            Vector3 inputDir = moveAxis.forward * ver + moveAxis.right * hor;
            Debug.DrawRay(transform.position,moveAxis.up, Color.green);
            Debug.DrawRay(transform.position,moveAxis.right, Color.red);
            Debug.DrawRay(transform.position,moveAxis.forward, Color.blue);
            inputDir = Vector3.ProjectOnPlane(inputDir,Vector3.up);
            if(entity.grounded == true)
            {
                    entity.body.useGravity = false;
                    if(inputDir.sqrMagnitude>0.1f && entity.ChangeState("Walking")) //Accelerating
                    {
                        if(sprint.GetInput()!=0) //Sprinting
                        {
                            Walk(inputDir, sprintSpeed, sprintAcceleration);
                        }
                        else //Walking
                        {
                            Walk(inputDir, walkSpeed, walkAcceleration);
                        }
                    }
                    else if(entity.body.linearVelocity.sqrMagnitude>0.1f && entity.ChangeState("Walking")) //Decelerating
                    {
                        Stop(deceleration);
                    }
            }
            else if(entity.grounded == false)
            {
                entity.body.useGravity = true;
                if(inputDir != Vector3.zero && entity.ChangeState("Gliding"))
                {
                    Debug.Log("asdasdas");
                    Glide(inputDir,walkSpeed,walkAcceleration);
                }
            }
            activeMoveSpeed = Vector3.ProjectOnPlane(entity.body.linearVelocity,Vector3.up).magnitude;
        }

        void Walk(Vector3 dir, float moveSpeed, float accel)
        {
            if(entity.GetState() == "Walking")
            {
                float velocity = entity.body.linearVelocity.magnitude;
                float accelerationRatio = (moveSpeed - (Mathf.Pow(velocity,2)/moveSpeed))/moveSpeed;
                float angle = Vector3.Angle(entity.body.linearVelocity,dir);
                float angleReduction = (180-Mathf.Pow(angle,2)/180)/180;

                velocity += accel * accelerationRatio * angleReduction * Time.fixedDeltaTime;
                velocity *= angleReduction;     

                Vector3 moveDir = Vector3.Lerp(entity.body.linearVelocity,dir,angleReduction);
                entity.body.linearVelocity = moveDir.normalized * velocity;
            }
        }

        void Stop(float deceleration)
        {
            entity.body.linearVelocity -= entity.body.linearVelocity * deceleration * Time.fixedDeltaTime;
            Debug.Log("STopp");
        }

        void Glide(Vector3 dir, float moveSpeed, float accel)
        {
            entity.body.linearVelocity += dir*moveSpeed*0.5f* Time.fixedDeltaTime;
        }

    }
}