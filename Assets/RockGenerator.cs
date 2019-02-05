using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    public GameObject[] rockType;
    public int numberOfRocks;
    public float maxHorizontalDistance;
    public float maxVerticalDistance;
    public float maxSize;
    public float time;
    public bool start = false;
    List<GameObject> rocks = new List<GameObject>();
    float timer;
    void Start()
    {
        for(int i = 0; i < numberOfRocks; i++)
        {
            GameObject currentRock = Instantiate(rockType[Random.Range(0, rockType.Length - 1)],transform);
            currentRock.SetActive(false);
            currentRock.transform.position = new Vector3(transform.position.x + Random.Range(-maxHorizontalDistance, maxHorizontalDistance), transform.position.y, transform.position.z + Random.Range(-maxVerticalDistance, maxVerticalDistance));
            currentRock.transform.rotation = Quaternion.EulerAngles(Random.RandomRange(0, 360), Random.RandomRange(0, 360), Random.RandomRange(0, 360));
            float size = Random.RandomRange(1, maxSize);
            currentRock.transform.localScale = new Vector3(size, size, size);
            rocks.Add(currentRock);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > time && start)
        {
            //Select one rock
            rocks[Random.RandomRange(0, rocks.Count - 1)].SetActive(true);
            timer = 0;
        }
    }
}
