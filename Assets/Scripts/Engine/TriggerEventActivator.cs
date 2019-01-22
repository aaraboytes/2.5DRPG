using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventActivator : MonoBehaviour
{
    public GameEvent triggerEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerEvent.StartEvent();
            Destroy(gameObject);
        }
    }
}
