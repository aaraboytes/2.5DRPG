using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class mainMenuManager : MonoBehaviour
{
    string path;
    public GameObject continueButton;
    private void Start()
    {
        path = Application.persistentDataPath + "/save.owo";
        if (File.Exists(path))
        {
            continueButton.SetActive(true);
        }
    }
    public void NewGame()
    {
        FindObjectOfType<FadeManager>().FadeOut("Pasillo_1");
        GameManager._instance.SetNextPlayerPosition(new Vector3(0, 0.5f, -2.5f));
    }
    public void Continue()
    {
        GameManager._instance.LoadSavedScene();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
