using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enigma
{
    public class Entity : MonoBehaviour
    {
        [Header("Entity")]

        [SerializeField]
        private string[] currentState = new string[25];// = "Idle";
        private string[] previousState = new string[25];
        [System.NonSerialized]
        public bool actionLock = false;

        void Start()
        {
            
        }

        void LateUpdate()
        {
            for(int i=0; i<currentState.Length; i++)
            {
                previousState[i] = currentState[i];
            }
        }

        public bool ChangeState(
            string state, string requiredState = null, bool overrideLock = false, int layerIndex = 0)
        {
            if(actionLock == true && overrideLock == false)
                return false;
            if(requiredState != GetState() && requiredState != null)
                return false;
            currentState[layerIndex] = state;
            return true;
        }

        public string GetState(int layerIndex = 0)
        {
            return currentState[layerIndex];
        }

        public string GetPreviousSate(int layerIndex = 0)
        {
            return previousState[layerIndex];
        }
    }
}