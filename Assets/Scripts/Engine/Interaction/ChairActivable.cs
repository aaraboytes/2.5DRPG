using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairActivable : Activable
{
    public Transform sitPosition;
    public Dialogue dialogue;
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
        player.gameObject.GetComponent<Animator>().SetFloat("yVel", -0.1f);
        player.gameObject.GetComponent<Animator>().SetFloat("xVel", 0);
        player.gameObject.GetComponent<Animator>().SetBool("moving", false);
        fader.FadeIn();
        //Start dialogue with Dr.
        DialogueManger._instance.StartConversation(dialogue);
    }
}
