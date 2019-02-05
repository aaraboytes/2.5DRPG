using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventActivator : MonoBehaviour
{
    public bool destroy = true;
    public GameEvent triggerEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerEvent.StartEvent();
            if(destroy)
               Destroy(gameObject);
        }
    }
}
