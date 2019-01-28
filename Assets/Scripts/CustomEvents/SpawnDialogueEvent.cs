using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDialogueEvent : GameEvent
{
    public Dialogue dialogue;
    public override void StartEvent()
    {
        DialogueManger._instance.StartConversation(dialogue);
    }
}
