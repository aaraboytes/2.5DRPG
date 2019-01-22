using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactuable : MonoBehaviour {
    public Dialogue dialogue;
    abstract public void InitConversation();
}
