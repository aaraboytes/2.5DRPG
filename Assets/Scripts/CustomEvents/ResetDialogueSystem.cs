using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDialogueSystem : GameEvent
{
    public override void StartEvent()
    {
        DialogueManger._instance.ResetDialogueSystem();
    }
}
