using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrFirstTalkEvent : GameEvent
{
    public Dialogue[] drConversation;
    public override void StartEvent()
    {
        DialogueManger._instance.StartConversation(drConversation);
    }
}
