using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarrafonActivable : Activable
{
    public Sprite[] garrafonImages;
    int index = 0;
    SpriteRenderer spr;
    Interactuable interactuable;
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        interactuable = GetComponent<Interactuable>();
    }
    public override void Activate()
    {
        if (index < garrafonImages.Length - 1)
        {
            index++;
        }
        else
        {
            interactuable.dialogue.sentences = new string[1];
            interactuable.dialogue.sentences[0] = "El garrafón se encuentra vacío";
        }
        spr.sprite = garrafonImages[index];
    }
}
