using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door properties")]
    public string nextScene;
    public Vector3 nextPlayerPos;
  
    public bool isDoorOpened = false;
    Interactuable interactuable;

    private void Start()
    {
        interactuable = GetComponent<Interactuable>();
        if (!isDoorOpened)
            CloseDoor();
        else
            OpenDoor();
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
        interactuable.conversation[0].sentence = "Esta abierto";
    }
    public void CloseDoor() {
        isDoorOpened = false;
    }
}
