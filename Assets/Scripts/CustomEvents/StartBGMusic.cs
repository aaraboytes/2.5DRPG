using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGMusic : GameEvent
{
    public AudioClip audio;
    public override void StartEvent()
    {
        if (GameManager._instance.gameObject.GetComponent<AudioSource>().clip != audio)
        {
            GameManager._instance.SetBGMusic(audio);
            GameManager._instance.PlayBGMusic();
        }
    }
}
