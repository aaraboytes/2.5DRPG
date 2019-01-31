using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawnConversation : MonoBehaviour
{
    public Dialogue[] conversation;
    public bool destroy = true;
    private void OnTriggerEnter(Collider other)
    {
        DialogueManger._instance.StartConversation(conversation);
        if(destroy)
            Destroy(gameObject);
    }
}
