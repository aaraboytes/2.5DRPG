using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class PlayerData {
    public float[] position = new float[3];
    public int health;
    public int[] items = new int[20];
    public string sceneName;

    public PlayerData(PlayerControllerSuperTwoD player)
    {
        position[0] = player.gameObject.transform.position.x;
        position[1] = player.gameObject.transform.position.y;
        position[2] = player.gameObject.transform.position.z;
        items = player.GetItems();
        health = player.GetHealth();
        sceneName = SceneManager.GetActiveScene().name;
    }
    public PlayerData()
    {
        position[0] = 0;
        position[1] = 0;
        position[2] = 0;
        items = new int[20];
        health = 4;
        sceneName = "";
    }
}
