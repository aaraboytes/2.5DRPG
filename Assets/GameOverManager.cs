using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Continue()
    {
        GameManager._instance.LoadSavedScene();
    }
    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
