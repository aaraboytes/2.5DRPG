using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDoor : MonoBehaviour
{
    [Header("Door properties")]
    public string nextScene;
    public Vector3 nextPlayerPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<FadeManager>().FadeOut(nextScene);             //Apply camera fade out effect to another scene
            GameManager._instance.SetNextPlayerPosition(nextPlayerPos);     //Set the next player position in the next scene
        }
    }
}
