using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    PlayerData currentPlayerData;
    PlayerControllerSuperTwoD player;
    private void Awake()
    {
        if (_instance != this)
            _instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void SetCurrentPlayerData(PlayerData pd)
    {
        currentPlayerData = pd;
    }
    public void SetCurrentPlayer(PlayerControllerSuperTwoD _player)
    {
        player = _player;
    }
    public void LoadSavedElements()
    {
        PlayerData pd = SaveSystem.Load();
        Vector3 savedPosition = new Vector3(pd.position[0], pd.position[1], pd.position[2]);
        player.transform.position = savedPosition;
        player.SetHealth(pd.health);
        player.SetItems(pd.items);
        FindObjectOfType<Inventory>().InitializeInventory();
    }
}
