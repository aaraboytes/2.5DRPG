using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHpEven : GameEvent
{
    public override void StartEvent()
    {
        HPManager._instance.TurnOn();
    }
}
