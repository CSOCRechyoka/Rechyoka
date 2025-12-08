using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enigma
{
    public class FPSLookAction : Action
    {
        Entity cam;
        [Header("Inputs")]
        public ActionInput horizontal;
        public ActionInput vertical;
        [Header("Variables")]
        public float turnSpeed;
        public float turnTime;

        public Vector2 angleClamp;
        
        public Transform orbitTarget;
        public Transform lookTarget;
        public float lookTime;

        float hor,ver;
        
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if(lookTarget != null)
            {
                StartCoroutine(Look(lookTarget, lookTime));
            }

            hor = horizontal.GetInput();
            ver = vertical.GetInput();
            StartCoroutine(Turn(hor,ver));
            transform.position = orbitTarget.position;
        }

        // void FixedUpdate()
        // {
        //     transform.position = orbitTarget.position;
        // }

        Vector3 lookVelocity;
        IEnumerator Look(Transform target, float time)
        {
            Vector3 targetDir = -(transform.position - target.position).normalized;
            Debug.DrawRay(transform.position,targetDir,Color.blue);
            transform.forward = Vector3.SmoothDamp(transform.forward, targetDir,ref lookVelocity, time, 180, Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        Vector2 turnVelo;
        Vector2 angles;
        IEnumerator Turn(float turnX,float turnY)
        {
            angles.x = Mathf.SmoothDamp(angles.x,turnX*turnSpeed,ref turnVelo.x,turnTime, 180, Time.deltaTime);
            Quaternion xRot = Quaternion.AngleAxis(angles.x,Vector3.up);
            
            transform.forward = 
                    xRot * transform.forward;

            angles.y = Mathf.SmoothDamp(angles.y,-turnY*turnSpeed,ref turnVelo.y,turnTime, 180, Time.deltaTime);
            Quaternion yRot = Quaternion.AngleAxis(angles.y,transform.right);

            //Clamp
            Vector3 planeFwd = Vector3.ProjectOnPlane(transform.forward,Vector3.up).normalized;
            float yPredicted = Vector3.SignedAngle(planeFwd,yRot * transform.forward,transform.right);
            Debug.Log(yPredicted);

            if(yPredicted < angleClamp.x || yPredicted > angleClamp.y)
            {
                Quaternion clampedYRot = yPredicted < angleClamp.x ? Quaternion.AngleAxis(angleClamp.x,transform.right) : Quaternion.AngleAxis(angleClamp.y,transform.right) ;
                transform.forward = Vector3.Slerp(clampedYRot * planeFwd,yRot * transform.forward,0.5f);
            }    
            else
            {
                transform.forward = yRot*transform.forward;
            }

            yield return null;
        }
    }
}