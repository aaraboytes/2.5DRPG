using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkWithF24 : GameEvent {
    PlayerControllerSuperTwoD p;
    CharacterController player;
    public Dialogue[] conversation;
    public float speed;
    public Transform target;
    bool startMovingPlayer = false;
    private void Start()
    {
        p = FindObjectOfType<PlayerControllerSuperTwoD>();
        player = p.GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (startMovingPlayer)
        {
            player.Move(-Vector3.forward * speed * Time.deltaTime);
            p.GetComponent<Animator>().SetInteger("up", -1);
            p.GetComponent<Animator>().SetBool("moving", true);
        }
        if (Vector3.Distance(target.position, player.transform.position) <= 0.5f)
        {
            startMovingPlayer = false;
            p.enabled = true;
        }
    }
    public override void StartEvent()
    {
        p.enabled = false;
        startMovingPlayer = true;
        DialogueManger._instance.StartConversation(conversation);
    }
}
