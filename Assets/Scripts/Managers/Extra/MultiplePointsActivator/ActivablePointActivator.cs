using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivablePointActivator : Activable
{
    public Dialogue[] optionalConversation;
    public override void Activate()
    {
        if(optionalConversation.Length!= 0)
        {
            DialogueManger._instance.StartConversation(optionalConversation);
        }
        MultiplePointsActivatorManager._instance.RegisterNewPoint();
    }
}
