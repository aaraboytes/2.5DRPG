using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightModifier : MonoBehaviour
{
    public Color initialColor;
    public Color endColor;
    Light light;
    private void Start()
    {
        light = GetComponent<Light>();
        
    }
    private void Update()
    {
        Color currentColor = light.color;
        light.color = Color.Lerp(currentColor, endColor, 0.01f);
    }
}
