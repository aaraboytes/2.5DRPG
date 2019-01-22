using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnPlayerEvent : GameEvent
{
    float normalSpeed;
    FollowCamera camScript;
    private void Start()
    {
        camScript = Camera.main.GetComponent<FollowCamera>();
        normalSpeed = camScript.speed;
    }
    public override void StartEvent()
    {
        Transform player = FindObjectOfType<PlayerControllerSuperTwoD>().transform;
        camScript.SetTarget(player);
        camScript.speed = normalSpeed;
    }
}
