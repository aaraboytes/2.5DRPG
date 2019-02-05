using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GoToAnotherScene : GameEvent
{
    string nextScene;
    Vector3 nextPos;
    PlayerControllerSuperTwoD player;
    public GoToAnotherScene(string _nextScene,Vector3 _nextPos, PlayerControllerSuperTwoD p)
    {
        nextPos = _nextPos;
        nextScene = _nextScene;
        player = p;
    }
    public override void StartEvent()
    {
        FindObjectOfType<FadeManager>().FadeOut(nextScene);             //Apply camera fade out effect to another scene
        GameManager._instance.SetNextPlayerPosition(nextPos);     //Set the next player position in the next scene
        GameManager._instance.SetCurrentItems(player.GetItemsList());
        GameManager._instance.SetPlayerHealth(player.GetCurrentHealth());
        GameManager._instance.SetCurrentPages(player.GetPages());
    }
}
public class Bluehole : GameEvent
{
    public string nextScene;
    public Vector3 nextPosition;
    PlayerControllerSuperTwoD player;
    public Animator anim;
    public AudioClip clip;
    public GameObject rocks;
    public Dialogue[] conversation;
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        player = FindObjectOfType<PlayerControllerSuperTwoD>();
    }
    private void OnTriggerEnter(Collider other)
    {
        audio.clip = clip;
        audio.loop = true;
        audio.volume = 0.6f;
        audio.Play();
        anim.SetTrigger("move");
        rocks.GetComponent<RockGenerator>().start = true;
        GetComponent<Collider>().enabled = false;
        player.paused = true;

        Invoke("StartEvent", 8.0f);
    }
    public override void StartEvent()
    {
        DialogueManger._instance.AddEvent(new GoToAnotherScene(nextScene, nextPosition, player));
        DialogueManger._instance.StartConversation(conversation);
    }
}
