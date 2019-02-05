using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEnding : GameEvent
{
    public GameObject[] normalLights;
    public GameObject[] alertLights;
    public Dialogue[] conversation;
    public GameObject[] explosions;
    float timer;
    public float timeBtwExplosions;
    public AudioClip explosionSound;
    public AudioClip portalSound;
    public AudioClip music;
    public Animator anim;
    AudioSource audio;
    [Header("Scene")]
    public string nextSceneName;
    public float timeForNextScene;
    float timerScene;
    bool startExploding = false;
    int currentIndex = 0;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (startExploding)
        {
            timerScene += Time.deltaTime;
            if (timer > timeBtwExplosions && currentIndex<explosions.Length)
            {
                explosions[currentIndex].SetActive(true);
                if (currentIndex == explosions.Length - 1)
                {
                    audio.PlayOneShot(portalSound, 1.0f);
                }
                else
                {
                    audio.PlayOneShot(explosionSound, 1.0f);
                }
                currentIndex++;
                timer = 0;
            }
        }
        if (timerScene > timeForNextScene)
        {
            GoToOtherScene();
        }
    }
    public override void StartEvent()
    {
        FindObjectOfType<PlayerControllerSuperTwoD>().paused = true;
        anim.SetTrigger("start");
        Invoke("Alert", 5.0f);
    }
    public void Alert()
    {
        GameManager._instance.SetBGMusic(music);
        GameManager._instance.PlayBGMusic();
        DialogueManger._instance.StartConversation(conversation);
        for(int i = 0; i < normalLights.Length; i++)
        {
            normalLights[i].SetActive(false);
            alertLights[i].SetActive(true);
        }
        startExploding = true;
    }
    public void GoToOtherScene()
    {
        FindObjectOfType<FadeManager>().FadeOut(nextSceneName);
    }
}
