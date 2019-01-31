using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    //Player
    PlayerData currentPlayerData;
    PlayerControllerSuperTwoD player;
    Vector3 playerPos;
    //Audio
    AudioSource audio;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerControllerSuperTwoD>();
        if (player)
            playerPos = player.transform.position;
        audio = GetComponent<AudioSource>();
    }
    #region Load player info
    public void SetCurrentPlayerData(PlayerData pd)
    {
        currentPlayerData = pd;
    }
    public void SetCurrentPlayer(PlayerControllerSuperTwoD _player)
    {
        player = _player;
    }
    public void LoadSavedScene()
    {
        PlayerData pd = SaveSystem.Load();
        SceneManager.LoadScene(pd.sceneName);
    }
    public void LoadSavedElements()
    {
        PlayerData pd = SaveSystem.Load();
        Vector3 savedPosition = new Vector3(pd.position[0], pd.position[1], pd.position[2]);
        
        //player.transform.position = savedPosition;
        player.SetHealth(pd.health);
        player.SetItems(pd.items);
        FindObjectOfType<Inventory>().InitializeInventory();
    }
    #endregion
    #region Transition between scenes
    public void SetNextPlayerPosition(Vector3 nextPlayerPos)
    {
        playerPos = nextPlayerPos;
    }
    public void SetPlayerPosition()
    {
        if (player == null)
            player = FindObjectOfType<PlayerControllerSuperTwoD>();
        if(player)
            player.gameObject.transform.position = playerPos;
    }
    public void GameOverScene()
    {
        FindObjectOfType<FadeManager>().FadeOut("GameOver");
    }
    #endregion
    #region Audio
    public void SetBGMusic(AudioClip music)
    {
        audio.clip = music;
    }
    public void PlayBGMusic() { audio.loop = true; audio.Play(); }
    public void StopBGMusic() { audio.Stop(); }
    #endregion
}
