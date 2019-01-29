﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingAbsorbedEvent : GameEvent
{
    public Dialogue[] conversation;
    public GameEvent goToAnotherScene;
    public override void StartEvent()
    {
        DialogueManger._instance.AddEvent(goToAnotherScene);
        DialogueManger._instance.StartConversation(conversation);
    }
}
