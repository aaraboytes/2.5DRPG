using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AnimationLauncher : GameEvent
{
    GameObject objToBeTriggered;
    string triggersName;
    public AnimationLauncher(GameObject obj,string trigger)
    {
        objToBeTriggered = obj;
        triggersName = trigger;
    }
    public override void StartEvent()
    {
        objToBeTriggered.GetComponent<Animator>().SetTrigger(triggersName);
    }
}
public class DialogueLaunchAnimation : GameEvent
{
    public Dialogue dialogue;
    public GameObject objToBeTriggered;
    public string triggersName;
    public override void StartEvent()
    {
        AnimationLauncher animationLauncher = new AnimationLauncher(objToBeTriggered,triggersName);
        DialogueManger._instance.AddEvent(animationLauncher);
        DialogueManger._instance.StartConversation(dialogue);
    }
}
