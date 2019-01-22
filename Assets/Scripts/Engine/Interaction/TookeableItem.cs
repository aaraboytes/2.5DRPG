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
        dialogue.sentences = new string[1];
        dialogue.sentences[0] = ("Has recogido un " + item.name);
        interact.dialogue = dialogue;
        gameObject.SetActive(false);
    }
    public void SetFailureDialogue()
    {
        Dialogue dialogue = new Dialogue();
        dialogue.name = item.name;
        dialogue.photo = item.itemImg;
        dialogue.sentences = new string[1];
        dialogue.sentences[0] = ("Parece que no tienes mas espacio en tu inventario");
        interact.dialogue = dialogue;
    }
}
