using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : MonoBehaviour
{
    public GameEvent() { }
    public abstract void StartEvent();
}
