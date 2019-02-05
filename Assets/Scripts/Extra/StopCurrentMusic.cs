using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCurrentMusic : MonoBehaviour
{
    private void Start()
    {
        GameManager._instance.StopBGMusic();
    }
}
