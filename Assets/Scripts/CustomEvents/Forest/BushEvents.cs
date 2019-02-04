using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToOtherScene : GameEvent
{
    Animator anim;
    public MoveToOtherScene(Animator _anim)
    {
        anim = _anim;
    }
    public override void StartEvent()
    {
        anim.SetTrigger("move");
    }
}
public class BushEvents : GameEvent
{
    public Dialogue[] conversation;

    public override void StartEvent()
    {
        
    }
    public void spawnDialogue (){
        FindObjectOfType<PlayerControllerSuperTwoD>().paused = true;
        DialogueManger._instance.AddEvent(new MoveToOtherScene(GetComponent<Animator>()));
        DialogueManger._instance.StartConversation(conversation);
    }
}
