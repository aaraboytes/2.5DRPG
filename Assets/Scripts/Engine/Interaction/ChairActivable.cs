using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairActivable : Activable
{
    public Transform sitPosition;
    public Dialogue dialogue;
    public GameEvent goToNextRoom;
    PlayerControllerSuperTwoD player;
    private void Start()
    {
        player = FindObjectOfType<PlayerControllerSuperTwoD>();
    }
    public override void Activate()
    {
        FadeManager fader = FindObjectOfType<FadeManager>();
        fader.FadeOut();
        //Sit the player
        player.transform.position = sitPosition.position;
        player.paused = true;
        player.gameObject.GetComponent<Animator>().SetInteger("up", 1);
        player.gameObject.GetComponent<Animator>().SetInteger("up", 0);
        fader.FadeIn();
        //Start dialogue with Dr.
        DialogueManger._instance.AddEvent(goToNextRoom);
        DialogueManger._instance.StartConversation(dialogue);
    }
}
