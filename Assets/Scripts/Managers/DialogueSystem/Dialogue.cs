using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue{
    public Sprite photo;
    public string name;
    public AudioClip audioClip;
    [TextArea(3,6)]
    public string[] sentences;
}
