using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

    public GameObject destroyedVersion;
    public float Force;
    public float Radius;
    
   

    void OnTriggerEnter(Collider other)
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        GetComponent<Rigidbody>().AddExplosionForce(Force, transform.position, Radius);
        Destroy(gameObject);
        
    }
}
