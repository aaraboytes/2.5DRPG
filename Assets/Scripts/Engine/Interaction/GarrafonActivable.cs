using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarrafonActivable : Activable
{
    public Sprite[] garrafonImages;
    int index = 0;
    SpriteRenderer spr;
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }
    public override void Activate()
    {
        if (index < garrafonImages.Length - 1)
        {
            index++;
            if(index == garrafonImages.Length-1)
            {
                gameObject.AddComponent<ConversationInteractuable>();
                gameObject.GetComponent<ConversationInteractuable>().conversation = new Dialogue[1];
                Dialogue dialogue = new Dialogue();
                dialogue.name = "Garrafón vacío";
                dialogue.photo = garrafonImages[5];
                dialogue.sentence = "Parece que el garrafon se encuentra vacio";
                gameObject.GetComponent<ConversationInteractuable>().conversation[0] = dialogue;
            }
        }
        spr.sprite = garrafonImages[index];
    }
}
