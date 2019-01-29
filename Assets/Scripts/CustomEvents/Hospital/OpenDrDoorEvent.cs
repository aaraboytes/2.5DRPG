using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class OpenDrDoorEvent : GameEvent
{
    public Transform drDoor;
    [Header("Secretaria")]
    public Dialogue[] conversation;
    public Interactuable secretary;
    public Dialogue[] newSecretaryConversation;
    [Header("Evento adicional")]
    public GameEvent focusOnPlayer;

    public override void StartEvent()
    {
        Debug.Log("OpenDrDoor event started");
        drDoor.GetComponent<Door>().OpenDoor();
        //Focus camera to the door
        Camera.main.gameObject.GetComponent<FollowCamera>().speed = 0.05f;
        Camera.main.gameObject.GetComponent<FollowCamera>().SetTarget(drDoor);
        //Start conversation with secretary
        DialogueManger._instance.StartConversation(conversation);
        DialogueManger._instance.AddEvent(focusOnPlayer);
        //Change dialogue to secretary
        secretary.conversation = newSecretaryConversation;
    }
}
