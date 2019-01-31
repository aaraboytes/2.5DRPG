using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPointActivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MultiplePointsActivatorManager._instance.RegisterNewPoint();
        }
    }
}
