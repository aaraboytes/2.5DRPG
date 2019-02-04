using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {
    public GameObject pausePanel;
    public GameObject[] cursorpositions;
    public Image cursor;
    PlayerControllerSuperTwoD player;
    bool pause = false;
    int cursorIndex = 0;

    bool inventory = false;
    public GameObject inventoryPanel;

    void Start () {
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        player = GameObject.FindObjectOfType<PlayerControllerSuperTwoD>();
	}

    void Update() {
        #region Pause and inventory manager
        if(player.paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && pause)
                Resume();
            if (Input.GetKeyDown(KeyCode.C) && inventory)
                HideInventory();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !inventory)
                Pause();
            if (Input.GetKeyDown(KeyCode.C) && !pause)
                ShowInventory();
        }
        #endregion
        #region Pause control
        if (pause)
        {
            cursor.transform.position = cursorpositions[cursorIndex].transform.position;
            if (Input.GetKeyDown(KeyCode.UpArrow) && cursorIndex > 0)
                cursorIndex--;
            if (Input.GetKeyDown(KeyCode.DownArrow) && cursorIndex < 4)
                cursorIndex++;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z)){
                switch (cursorIndex){
                    case 0:
                        Resume();
                        break;
                    case 1:
                        Save();
                        break;
                    case 2:
                        Quit();
                        break;
                }
            }
        }
        #endregion
    }
    #region Pause methods
    void Pause()
    {
        pause = true;
        pausePanel.SetActive(true);
        player.paused = true;
        player.gameObject.GetComponent<Animator>().SetBool("moving", false);
    }
    void Resume()
    {
        pause = false;
        cursorIndex = 0;
        pausePanel.SetActive(false);
        player.paused = false;
    }
    void Save()
    {
        SaveSystem.Save(player);
        Resume();
    }
    void Quit()
    {
        FindObjectOfType<FadeManager>().FadeOut("MainMenu");
    }
    #endregion
    #region Inventory methods
    void ShowInventory()
    {
        inventory = true;
        player.paused = true;
        inventoryPanel.SetActive(true);
    }
    void HideInventory()
    {
        inventory = false;
        player.paused = false;
        inventoryPanel.SetActive(false);
    }
    #endregion
}
