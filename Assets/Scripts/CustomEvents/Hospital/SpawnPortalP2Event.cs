using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortalP2Event : GameEvent
{
    public GameObject portal;
    public Dialogue[] conversation;
    public GameEvent goToAnotherSceneEvent;
    public override void StartEvent()
    {
        portal.GetComponent<Animator>().SetTrigger("Increase");
        DialogueManger._instance.AddEvent(goToAnotherSceneEvent);
        DialogueManger._instance.StartConversation(conversation);
    }
}
