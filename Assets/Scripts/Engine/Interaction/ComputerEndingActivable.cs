using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerEndingActivable : Activable
{
    public Dialogue[] firstConversation;
    public Dialogue[] secondConversation;
    public MetalDoor firstDoor, secondDoor;
    public GameEvent eventAfterFirstConversation;
    bool activated = false;

    public override void Activate()
    {
        if (!activated)
        {
            DialogueManger._instance.AddEvent(eventAfterFirstConversation);
            DialogueManger._instance.StartConversation(firstConversation);
            firstDoor.opened = true;
            secondDoor.opened = true;
            activated = true;
        }
        else
        {
            DialogueManger._instance.StartConversation(secondConversation);
        }
    }
}
