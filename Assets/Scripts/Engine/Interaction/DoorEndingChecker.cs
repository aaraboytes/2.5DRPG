using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEndingChecker : Activable
{
    public Vector3 nextPlayerPos;
    public int numberOfPages;
    public string goodEndingScene;
    public string badEndingScene;
    public string currentScene;
    public bool opened = false;
    public Dialogue[] conversation;
    Animator anim;
    PlayerControllerSuperTwoD player;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerControllerSuperTwoD>();
    }
    public override void Activate()
    {
        if (opened)
            OpenDoor();
        else
            CloseDoor();
    }
    void CloseDoor()
    {
        anim.SetTrigger("close");
        DialogueManger._instance.StartConversation(conversation);
    }
    public void OpenDoor()
    {
        if(player.GetPages()>= numberOfPages)
        {
            currentScene = goodEndingScene;
        }
        else
        {
            currentScene = badEndingScene;
        }
        Invoke("GoToNextScene", 2.0f);
    }
    void GoToNextScene()
    {
        FindObjectOfType<FadeManager>().FadeOut(currentScene);             //Apply camera fade out effect to another scene
        GameManager._instance.SetNextPlayerPosition(nextPlayerPos);     //Set the next player position in the next scene
        GameManager._instance.SetCurrentItems(player.GetItemsList());
        GameManager._instance.SetPlayerHealth(player.GetCurrentHealth());
        GameManager._instance.SetCurrentPages(player.GetPages());
    }
}
