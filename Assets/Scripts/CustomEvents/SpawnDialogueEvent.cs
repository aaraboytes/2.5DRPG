using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDialogueEvent : GameEvent
{
    public Dialogue[] conversation;
    public override void StartEvent()
    {
        DialogueManger._instance.StartConversation(conversation);
    }
}
