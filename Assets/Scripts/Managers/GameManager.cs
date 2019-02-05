using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager _instance;
    //Player
    public bool loadData = false;
    PlayerData currentPlayerData;
    PlayerControllerSuperTwoD player;
    public Vector3 playerPos;
    [SerializeField]
    List<int> currentItems = new List<int>();
    int currentHealth = 5;
    int currentPages = 0;
    //Audio
    AudioSource audio;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if(_instance!=this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        for (int i = 0; i < 20; i++)
            currentItems.Add(0);
        if (player == null)
            player = FindObjectOfType<PlayerControllerSuperTwoD>();
        if (player)
            playerPos = player.transform.position;
        
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
        SetCurrentPlayerData(pd);
        Vector3 savedPosition = new Vector3(pd.position[0], pd.position[1], pd.position[2]);
        SetNextPlayerPosition(savedPosition);
        SceneManager.LoadScene(pd.sceneName);
    }
    public void LoadSavedElements()
    {
        PlayerData pd;
        if (currentPlayerData == null)
        {
            pd = SaveSystem.Load();
            SetCurrentPlayerData(pd);
        }
        else
            pd = currentPlayerData;
        if(player == null && FindObjectOfType<PlayerControllerSuperTwoD>())
        {
            player = FindObjectOfType<PlayerControllerSuperTwoD>();
        }
        player.SetHealth(pd.health);
        player.SetItems(pd.items);
        player.SetPages(pd.pages);
        FindObjectOfType<Inventory>().InitializeInventory();
        loadData = false;
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
    public void SetPlayerHealth(int health)
    {
        currentHealth = health;
    }
    public int GetPlayerHealth()
    {
        return currentHealth;
    }
    public void SetCurrentItems(List<int> items)
    {
        currentItems = items;
    }
    public List<int> GetCurrentItems()
    {
        return currentItems;
    }
    public void SetCurrentPages(int _pages)
    {
        currentPages = _pages;
    }
    public int GetCurrentPages()
    {
        return currentPages;
    }
    public void GameOverScene()
    {
        FindObjectOfType<FadeManager>().FadeOut("GameOver");
    }
    #endregion
    #region Audio
    public void SetBGMusic(AudioClip music)
    {
        if(audio.clip != music)
            audio.clip = music;
    }
    public void PlayBGMusic() { audio.loop = true; audio.Play(); }
    public void StopBGMusic() { audio.Stop(); }
    #endregion
}
