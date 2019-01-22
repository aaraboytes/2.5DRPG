using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivator : Activable
{
    public Door door;
    public GameEvent gameEvent;
    public override void Activate()
    {
        Debug.Log(name + " activated");
        door.OpenDoor();
        gameEvent.StartEvent();
    }
}
