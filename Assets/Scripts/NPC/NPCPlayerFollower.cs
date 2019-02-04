using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPlayerFollower : MonoBehaviour
{
    public Transform target;
    public float minDistance;
    public float speed;
    NavMeshAgent agent;
    Rigidbody body;
    private void Start()
    {
        if (target == null)
        {
            target = FindObjectOfType<PlayerControllerSuperTwoD>().transform;
        }
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.stoppingDistance = minDistance;
    }
    private void Update()
    {
        agent.SetDestination(target.position);
    }
}
