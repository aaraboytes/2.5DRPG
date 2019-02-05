using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GoToNextScene : GameEvent
{
    string scene;
    public GoToNextScene(string _scene)
    {
        scene = _scene;
    }
    public override void StartEvent()
    {
        FindObjectOfType<FadeManager>().FadeOut(scene);
    }
    
}
public class BadEnding : GameEvent
{
    public GameObject portal;
    public string nextScene;
    public Dialogue[] conversation;
    public GameObject l1, l2, l3;
    AudioSource audio;
    [Header("Spawn Portals")]
    public float time;
    public bool spawnPortals = false;
    public AudioClip clip;
    public AudioClip music;
    public float horizontalOffset;
    public float verticalOffset;
    public float maxSize;
    float timer= 0;
    List<GameObject> portals = new List<GameObject>();
    PlayerControllerSuperTwoD player;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        for (int i = 0; i < 50; i++)
        {
            GameObject currentPortal = Instantiate(portal, transform);
            currentPortal.SetActive(false);
            currentPortal.transform.position = new Vector3(transform.position.x + Random.Range(-horizontalOffset, horizontalOffset), transform.position.y, transform.position.z + Random.Range(-verticalOffset, verticalOffset));
            currentPortal.transform.rotation = Quaternion.EulerAngles(Random.RandomRange(0, 360), Random.RandomRange(0, 360), Random.RandomRange(0, 360));
            float size = Random.RandomRange(1, maxSize);
            currentPortal.transform.localScale = new Vector3(size, size, size);
            portals.Add(currentPortal);
        }
        player = FindObjectOfType<PlayerControllerSuperTwoD>();
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer > time && spawnPortals)
        {
            //Select one rock
            portals[Random.RandomRange(0, portals.Count - 1)].SetActive(true);
            audio.PlayOneShot(clip);
            timer = 0;
        }
    }
    public override void StartEvent()
    {
        l1.SetActive(true);
        l2.SetActive(true);
        l3.SetActive(true);
        spawnPortals = true;
        GameManager._instance.SetBGMusic(music);
        GameManager._instance.PlayBGMusic();
        player.paused = true;
        Invoke("GoToNextScene", 8.0f);
    }
    void GoToNextScene()
    {
        DialogueManger._instance.AddEvent(new GoToNextScene(nextScene));
        DialogueManger._instance.StartConversation(conversation);
    }
}
