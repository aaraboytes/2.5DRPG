using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class mainMenuManager : MonoBehaviour
{
    string path;
    public GameObject continueButton;
    public GameObject deletePanel;
    private void Start()
    {
        path = Application.persistentDataPath + "/save.owo";
        Debug.Log(path);
        if (!File.Exists(path))
        {
            continueButton.SetActive(false);
        }
    }
    public void NewGame()
    {
        FindObjectOfType<FadeManager>().FadeOut("Pasillo_1");
        GameManager._instance.SetNextPlayerPosition(new Vector3(0, 0.5f, -2.5f));
    }
    public void Continue()
    {
        GameManager._instance.loadData = true;
        GameManager._instance.LoadSavedScene();
    }
    public void Delete()
    {
        deletePanel.SetActive(true);
    }
    public void DeleteSaveData()
    {
        File.Delete(path);
        FindObjectOfType<FadeManager>().FadeOut("MainMenu");
    }
    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
