using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour {
    public Dialogue dialogue;
    public void InitConversation()
    {
        FindObjectOfType<DialogueManger>().StartConversation(dialogue);
    }
}
