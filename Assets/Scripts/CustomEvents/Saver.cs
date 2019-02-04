using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : GameEvent
{
    public bool saveOnSceneStart = false;
    void Start()
    {
        if (saveOnSceneStart)
            Invoke("StartEvent", 0.2f);
    }

    public override void StartEvent()
    {
        SaveSystem.Save(FindObjectOfType<PlayerControllerSuperTwoD>());
    }
}
