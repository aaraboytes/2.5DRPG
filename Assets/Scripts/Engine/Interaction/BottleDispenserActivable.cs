using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleDispenserActivable : Activable
{
    public GameObject[] bottles;
    int index = 0;
    PlayerControllerSuperTwoD player;
    Inventory inventory;
    private void Start()
    {
        player = FindObjectOfType<PlayerControllerSuperTwoD>();
        inventory = FindObjectOfType<Inventory>();
    }
    public override void Activate()
    {
        if (index >= bottles.Length)
            return;
        GameObject currentBottle = bottles[index];
        TookeableItem tookeableItem = currentBottle.GetComponent<TookeableItem>();
        if (inventory.AddItem())
        {
            player.AddItem(tookeableItem.item.id);
            tookeableItem.SetSuccessDialogue();
            index++;
        }
        else
            tookeableItem.SetFailureDialogue();
        DialogueManger._instance.StartConversation(currentBottle.GetComponent<Interactuable>().conversation);
    }
}
