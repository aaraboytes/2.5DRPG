using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortalEvent : GameEvent
{
    public GameObject portal;
    public Dialogue[] conversation;
    public GameEvent increasePortal;
    public Transform newCamPos;
    public AudioClip portalMusic;
    public override void StartEvent()
    {
        Camera.main.gameObject.GetComponent<FollowCamera>().SetTarget(portal.transform.GetChild(0).transform);
        Camera.main.gameObject.GetComponent<FollowCamera>().SetNewPosition(newCamPos);
        portal.SetActive(true);
        DialogueManger._instance.AddEvent(increasePortal);
        DialogueManger._instance.StartConversation(conversation);
        GameManager._instance.SetBGMusic(portalMusic);
        GameManager._instance.PlayBGMusic();
    }
}
