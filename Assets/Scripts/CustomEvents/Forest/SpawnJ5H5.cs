using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJ5H5 : GameEvent
{
    public GameObject F5H5;
    Transform player;
    void Start()
    {
        player = FindObjectOfType<PlayerControllerSuperTwoD>().transform;
        Invoke("StartEvent", 0.2f); 
    }
    public override void StartEvent()
    {
        F5H5.transform.position = player.position + Vector3.right *0.5f;
    }
}
