using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrFirstTalkEvent : GameEvent
{
    public Dialogue drDialogue;
    public override void StartEvent()
    {
        DialogueManger._instance.StartConversation(drDialogue);
    }
}
