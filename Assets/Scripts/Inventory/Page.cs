using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public Dialogue[] conversation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControllerSuperTwoD player = other.GetComponent<PlayerControllerSuperTwoD>();
            player.IncrementPage();
            DialogueManger._instance.StartConversation(conversation);
            Destroy(gameObject);
        }
    }
}
