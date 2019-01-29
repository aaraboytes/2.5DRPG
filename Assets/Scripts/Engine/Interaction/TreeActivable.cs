using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeActivable : Activable
{
    public GameObject apple;
    public Transform[] appleSpawner;
    public int numberOfApples;
    public Dialogue[] treeConversation;
    public override void Activate()
    {
        numberOfApples--;
        if (numberOfApples > 0)
        {
            Instantiate(apple, appleSpawner[(int)Random.Range(0, appleSpawner.Length)]);
        }
        else
        {
            DialogueManger._instance.StartConversation(treeConversation);
        }
    }
}
