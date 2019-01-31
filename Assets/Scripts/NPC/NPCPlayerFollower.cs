using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlayerFollower : MonoBehaviour
{
    public Transform target;
    public float speed;
    Rigidbody body;
    private void Start()
    {
        if (target == null)
            target = FindObjectOfType<PlayerControllerSuperTwoD>().transform;
        body = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        body.velocity = dir.normalized * speed;
    }
}
