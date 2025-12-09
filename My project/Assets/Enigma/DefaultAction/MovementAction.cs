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
                Debug.Log(accel*accelerationRatio);
                velocity += accel * accelerationRatio * Time.fixedDeltaTime;

                float angleReduction = (180-Mathf.Pow(Vector3.Angle(entity.body.linearVelocity,dir),2)/180)/180;
                velocity *= angleReduction;     

                entity.body.linearVelocity = dir.normalized * velocity;
            }
        }

        void Stop(float deceleration)
        {
            entity.body.linearVelocity -= entity.body.linearVelocity * deceleration * Time.fixedDeltaTime;
            Debug.Log("STopp");
        }

        void Glide(Vector3 dir, float moveSpeed, float accel)
        {
            Vector3 projVelocity = Vector3.ProjectOnPlane(entity.body.linearVelocity,Vector3.up);
            float velocity = projVelocity.magnitude;
            float accelerationRatio = moveSpeed - (Mathf.Pow(velocity,2)*Vector3.Dot(projVelocity,dir)/moveSpeed);
            velocity = accel * accelerationRatio * Time.fixedDeltaTime;

            float angleReduction = (180-Mathf.Pow(Vector3.Angle(entity.body.linearVelocity,dir),2)/180)/180;
            velocity *= angleReduction;     
            Vector3 horVelo = dir.normalized * velocity;
            entity.body.linearVelocity += horVelo;
        }

    }
}