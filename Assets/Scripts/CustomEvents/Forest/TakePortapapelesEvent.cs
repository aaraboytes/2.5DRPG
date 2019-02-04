using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePortapapelesEvent : GameEvent
{
    public Dialogue[] conversation;
    public GameObject obj;
    public Item item;
    PlayerControllerSuperTwoD player;
    private void Start()
    {
        player = FindObjectOfType<PlayerControllerSuperTwoD>();
    }
    public override void StartEvent()
    {
        DialogueManger._instance.StartConversation(conversation);
        player.AddItem(item.id);
        Destroy(obj);
    }
}
