using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookPlayer : MonoBehaviour
{
    public Transform target;
    private void Start()
    {
        if (target == null)
            target = FindObjectOfType<PlayerControllerSuperTwoD>().transform;
    }
    private void Update()
    {
        transform.LookAt(target);
    }
}
