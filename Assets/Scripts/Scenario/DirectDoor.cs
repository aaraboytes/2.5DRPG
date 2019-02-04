using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDoor : MonoBehaviour
{
    [Header("Door properties")]
    public string nextScene;
    public Vector3 nextPlayerPos;
    PlayerControllerSuperTwoD player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerControllerSuperTwoD>();
            FindObjectOfType<FadeManager>().FadeOut(nextScene);             //Apply camera fade out effect to another scene
            GameManager._instance.SetNextPlayerPosition(nextPlayerPos);     //Set the next player position in the next scene
            GameManager._instance.SetCurrentItems(player.GetItemsList());
            GameManager._instance.SetPlayerHealth(player.GetCurrentHealth());
            GameManager._instance.SetCurrentPages(player.GetPages());
        }
    }
}
