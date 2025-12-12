using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class ScriptIntreact : MonoBehaviour
{
    GameObject heldobject;
    public Camera cam;
    Ray razintr;
    public float holdingDistance;
    void pickupobject()
    {
        /*heldobject.position = cam.transform.position + cam.transform.forward * holdingDistance;*/
    }
    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.U))
        {
            razintr = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(razintr, out RaycastHit hit, 20f, 1 << 3))
            {
                UnityEngine.Debug.Log(hit.collider.gameObject.name + "a fost apucat");

            }
        }*/


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate()
    {
      /*if(heldobject != null)
            pickupobject();*/
    }
}
