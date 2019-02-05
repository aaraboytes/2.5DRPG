using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistancePointLight : MonoBehaviour
{
    Light light;
    Transform player;
    public float minDistance;
    public float maxDistance;
    float mag;
    float extra;

    void Start()
    {
        light = GetComponent<Light>();
        player = FindObjectOfType<PlayerControllerSuperTwoD>().transform;
        mag = maxDistance - minDistance;
        extra = minDistance / mag;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        //Calculate new alpha
        float alpha;
        if (distance > minDistance)
        {
            alpha = 1 - (distance / mag) + extra;
        }
        else
        {
            alpha = 1;
        }
        //Set new alpha to local color variables
        light.intensity = alpha;
    }
}
