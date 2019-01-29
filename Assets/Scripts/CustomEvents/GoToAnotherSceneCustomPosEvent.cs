using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToAnotherSceneCustomPosEvent : GameEvent
{
    public Vector3 newPos;
    public string anotherScene;
    public override void StartEvent()
    {
        FindObjectOfType<FadeManager>().FadeOut(anotherScene);             //Apply camera fade out effect to another scene
        GameManager._instance.SetNextPlayerPosition(newPos);     //Set the next player position in the next scene
    }
}
