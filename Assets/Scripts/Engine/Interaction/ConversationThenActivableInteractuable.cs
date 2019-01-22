using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationThenActivableInteractuable : Interactuable
{
    public GameEvent gameEvent;
    public override void InitConversation()
    {
        DialogueManger._instance.AddEvent(gameEvent);
        DialogueManger._instance.StartConversation(dialogue);
    }
}
