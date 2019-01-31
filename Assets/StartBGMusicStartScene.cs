using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGMusicStartScene : MonoBehaviour
{
    public AudioClip music;
    void Start()
    {
        GameManager._instance.SetBGMusic(music);
        GameManager._instance.PlayBGMusic();
    }
}
