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
        }

        void FixedUpdate()
        {
            transform.position = orbitTarget.position;
        }

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
            angles.x = Mathf.SmoothDamp(angles.x,turnX*turnSpeed,ref turnVelo.x,turnTime, 0, Time.deltaTime);
            Quaternion xRot = Quaternion.AngleAxis(angles.x,Vector3.up);
            transform.forward = 
                xRot * transform.forward;

            angles.y = Mathf.SmoothDamp(angles.y,-turnY*turnSpeed,ref turnVelo.y,turnTime, 0, Time.deltaTime);
            Quaternion yRot = Quaternion.AngleAxis(angles.y,transform.right);
            transform.forward = yRot*transform.forward;
            
            yield return new WaitForFixedUpdate();
        }
    }
}