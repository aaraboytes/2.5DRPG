using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new Item",menuName ="Items")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite itemImg;
    public string description;
}
