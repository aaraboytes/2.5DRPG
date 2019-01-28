using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToAnotherSceneEvent : GameEvent
{
    public string anotherScene;
    public override void StartEvent()
    {
        FindObjectOfType<FadeManager>().FadeOut(anotherScene);             //Apply camera fade out effect to another scene
        GameManager._instance.SetNextPlayerPosition(FindObjectOfType<PlayerControllerSuperTwoD>().transform.position);     //Set the next player position in the next scene
    }
}
