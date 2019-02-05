using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStartAnimation : MonoBehaviour
{
    public string triggersName;
    public AudioClip clip;
    public GameObject go;
    public bool audioPlay = true;
    Animator anim;
    AudioSource audio;
    private void Start()
    {
        if (go == null)
            anim = GetComponent<Animator>();
        else
            anim = go.GetComponent<Animator>();
        if (audioPlay)
        {
            audio = GetComponent<AudioSource>();
            audio.clip = clip;
            audio.loop = false;
            audio.playOnAwake = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(audioPlay)
                audio.PlayOneShot(clip, 0.4f);
            anim.SetTrigger(triggersName);
        }
    }
}
