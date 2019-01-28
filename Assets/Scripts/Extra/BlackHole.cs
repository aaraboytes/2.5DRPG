using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float force;
    List<Rigidbody> bodies = new List<Rigidbody>();
    private void Update()
    {
        foreach(Rigidbody body in bodies)
        {
            Vector3 dir = transform.position - body.position;
            body.AddForce(dir * force);
            body.AddTorque(Vector3.up * 2);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Rigidbody currentBody = other.GetComponent<Rigidbody>();
            if (!bodies.Contains(currentBody))
                bodies.Add(currentBody);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Rigidbody currentBody = other.GetComponent<Rigidbody>();
            if (bodies.Contains(currentBody))
                bodies.Remove(currentBody);
        }
    }
}
