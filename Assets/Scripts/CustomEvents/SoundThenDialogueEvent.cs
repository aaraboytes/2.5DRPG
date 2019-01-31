using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundThenDialogueEvent : GameEvent
{
    public AudioClip sound;
    public Dialogue[] conversation;
    AudioSource audio;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public override void StartEvent()
    {
        audio.PlayOneShot(sound, 1.0f);
        DialogueManger._instance.StartConversation(conversation);
    }
}
