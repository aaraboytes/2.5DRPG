using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushSecondEvents : MonoBehaviour
{
    AudioSource audio;
    public AudioClip clip;
    public Dialogue[] conversation;
    public GameObject wall;
    public GameObject interactuable;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void PausePlayer()
    {
        FindObjectOfType<PlayerControllerSuperTwoD>().paused = false;
    }
    public void PlayAudio(){
        audio.PlayOneShot(clip, 1.0f);
    }
    public void StartConversation()
    {
        wall.SetActive(false);
        interactuable.SetActive(false);
        DialogueManger._instance.StartConversation(conversation);
    }
}
