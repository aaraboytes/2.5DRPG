using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHolesGenerator : GameEvent
{
    public GameObject portal;
    AudioSource audio;
    [Header("Scene")]
    public string nextScene;
    public float sceneTime;
    float sceneTimer;
    [Header("Spawn Portals")]
    public float time;
    public AudioClip clip;
    public float horizontalOffset;
    public float verticalOffset;
    public float maxSize;
    List<GameObject> portals = new List<GameObject>();
    float timer = 0;
    // Start is called before the first frame update
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
    }
    public override void StartEvent()
    {
        
    }
    private void Update()
    {
        timer += Time.deltaTime;
        sceneTimer += Time.deltaTime;
        if (timer > time)
        {
            //Select one rock
            portals[Random.RandomRange(0, portals.Count - 1)].SetActive(true);
            audio.PlayOneShot(clip);
            timer = 0;
        }
        if (sceneTimer > sceneTime)
        {
            FindObjectOfType<FadeManager>().FadeOut(nextScene);
        }
    }
}
