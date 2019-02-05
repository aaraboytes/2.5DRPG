using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerActivable : Activable
{
    public MetalDoor door;
    public Material openMaterial;
    Renderer renderer;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public override void Activate()
    {
        door.opened = true;
        Debug.Log(renderer.sharedMaterials[0]);
        Debug.Log(renderer.sharedMaterials[1]);
        renderer.sharedMaterials[1] = openMaterial;
        GetComponent<AudioSource>().Play();
    }
}
