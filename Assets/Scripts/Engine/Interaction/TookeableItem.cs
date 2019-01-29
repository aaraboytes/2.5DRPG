using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TookeableItem : MonoBehaviour
{
    public Item item;
    Interactuable interact;
    private void Start()
    {
        interact = GetComponent<Interactuable>();
    }
    public void SetSuccessDialogue()
    {
        Dialogue dialogue = new Dialogue();
        dialogue.name = item.name;
        dialogue.photo = item.itemImg;
        dialogue.sentence = "Has recogido un " + item.name;
        interact.conversation = new Dialogue[1];
        interact.conversation[0] = dialogue;
        gameObject.SetActive(false);
    }
    public void SetFailureDialogue()
    {
        Dialogue dialogue = new Dialogue();
        dialogue.name = item.name;
        dialogue.photo = item.itemImg;
        dialogue.sentence = "Parece que no tienes mas espacio en tu inventario";
        interact.conversation = new Dialogue[1];
        interact.conversation[0] = dialogue;
    }
}
