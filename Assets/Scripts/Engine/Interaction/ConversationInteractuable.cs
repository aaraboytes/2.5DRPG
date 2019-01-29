using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationInteractuable : Interactuable
{
    public override void InitConversation()
    {
        DialogueManger._instance.StartConversation(conversation);
    }
}
