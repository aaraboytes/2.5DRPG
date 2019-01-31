using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class LookAtPlayer : GameEvent
{
    Transform player;
    FollowCamera cam;
    public LookAtPlayer(Transform playerPos,FollowCamera _cam)
    {
        player = playerPos;
        cam = _cam;
    }
    public override void StartEvent()
    {
        cam.SetTarget(player);
        cam.speed = 4;
    }
}
public class NoticeBadSlothsEvent : GameEvent
{
    public Transform lookSlothmans;
    public Dialogue[] thoughts;
    public AudioClip terrorMusic;
    Transform currentPlayerPos;
    PlayerControllerSuperTwoD player;
    FollowCamera cam;
    public override void StartEvent()
    {
        player = FindObjectOfType<PlayerControllerSuperTwoD>();
        cam = Camera.main.GetComponent<FollowCamera>();
        currentPlayerPos = player.transform;
        cam.SetTarget(lookSlothmans);
        cam.speed = 0.05f;
        player.paused = true;
        Invoke("LookSloths", 3.0f);

    }
    void LookSloths()
    {
        LookAtPlayer lookPlayer = new LookAtPlayer(player.transform, cam);
        DialogueManger._instance.AddEvent(lookPlayer);
        DialogueManger._instance.StartConversation(thoughts);
        GameManager._instance.SetBGMusic(terrorMusic);
        GameManager._instance.PlayBGMusic();
    }
}
