using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExample : MonoBehaviour
{
    public float bombRadius;
    public float bombExplosionTime;
    public float bombExplosionForce;
    public LayerMask layers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(BombLogic(bombRadius,bombExplosionTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BombLogic(float radius, float time)
    {
        yield return new WaitForSeconds(time);

        //Explosion Logic
        Collider[] colliderList = Physics.OverlapSphere(transform.position,radius,layers);
        foreach(Collider hit in colliderList)
        {
                if(hit.gameObject.GetComponent<Rigidbody>())
                {
                    Rigidbody body = hit.gameObject.GetComponent<Rigidbody>();
                    Debug.Log(hit.gameObject);
                    body.AddExplosionForce(bombExplosionForce,transform.position,radius,1000);
                }
        }
        Destroy(gameObject,0.1f);
        
    }
}
