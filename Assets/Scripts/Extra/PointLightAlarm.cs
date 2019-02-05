using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightAlarm : MonoBehaviour
{
    public float speed;
    public float intensityPower;
    Light light;
    float timer = 0;
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float intensity = (Mathf.Sin(timer * speed)+1)/2;
        light.intensity = intensity * intensityPower;
    }
}
