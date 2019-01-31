﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGuide : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float minDistance;
    Transform player;
    Rigidbody body;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerControllerSuperTwoD>().transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance<= minDistance)
        {
            Vector3 dir = target.position - transform.position;
            body.velocity = dir.normalized * speed;
        }
    }
}