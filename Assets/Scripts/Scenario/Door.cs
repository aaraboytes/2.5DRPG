using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door properties")]
    public string nextScene;
    public Vector3 nextPlayerPos;
    [TextArea()]
    public string textOfClosedDoor;
    public bool isDoorOpened = false;
    Interactuable interactuable;

    private void Start()
    {
        interactuable = GetComponent<Interactuable>();
        if (!isDoorOpened)
            CloseDoor();
    }
    public void PassDoor()
    {
        if (isDoorOpened)
        {
            FindObjectOfType<PlayerControllerSuperTwoD>().paused = true;    //Pause player
            FindObjectOfType<FadeManager>().FadeOut(nextScene);             //Apply camera fade out effect to another scene
            GameManager._instance.SetNextPlayerPosition(nextPlayerPos);     //Set the next player position in the next scene
        }
    }
    public void OpenDoor() {
        isDoorOpened = true;
        interactuable.dialogue.sentences = new string[0];
    }
    public void CloseDoor() {
        isDoorOpened = false;
        interactuable.dialogue.sentences = new string[1];
        interactuable.dialogue.sentences[0] = textOfClosedDoor;
    }
}
