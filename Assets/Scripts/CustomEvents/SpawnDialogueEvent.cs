using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDialogueEvent : GameEvent
{
    public Dialogue[] conversation;
    public bool startWhenSceneStarts = false;
    private void Start()
    {
        if (startWhenSceneStarts)
            Invoke("StartEvent", 0.2f);
    }
    public override void StartEvent()
    {
        DialogueManger._instance.StartConversation(conversation);
    }
}
