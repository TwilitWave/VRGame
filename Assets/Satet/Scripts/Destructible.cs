using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    public float Force;
    public float Radius;

    public void Destrory()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        GetComponentInChildren<Rigidbody>().AddExplosionForce(Force, transform.position, Radius);
        Destroy(this.gameObject);
    }
}