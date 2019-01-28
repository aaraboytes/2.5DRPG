using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartAnimation : MonoBehaviour
{
    public string triggersName;
    public AudioClip clip;
    Animator anim;
    AudioSource audio;
    private void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        audio.clip = clip;
        audio.loop = false;
        audio.playOnAwake = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audio.PlayOneShot(clip, 0.4f);
            anim.SetTrigger(triggersName);
        }
    }
}
