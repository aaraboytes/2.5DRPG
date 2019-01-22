using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public string firstScene;
    void NewGame()
    {
        SaveSystem.NewGame();
        SceneManager.LoadScene(firstScene);
    }
    void Continue()
    {
        PlayerData pd = SaveSystem.Load();
        if (pd == null)
        {
            NewGame();
        }
        pd = SaveSystem.Load();
        GameManager._instance.SetCurrentPlayerData(pd);
        SceneManager.LoadScene(pd.sceneName);
    }
}
