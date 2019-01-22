using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public GameObject fader;
    Animator faderAnim;

    bool goToNextScene = false;
    string nextSceneName;

    void Start()
    {
        faderAnim = fader.GetComponent<Animator>();
    }
    
    public void FadeOut()
    {
        faderAnim.SetTrigger("FadeOut");
    }

    public void FadeOut(string nextScene)
    {
        faderAnim.SetTrigger("FadeOut");
        goToNextScene = true;
        nextSceneName = nextScene;
    }

    public void FadeIn()
    {
        faderAnim.SetTrigger("FadeIn");
    }

    public void FadeInToNextScene()
    {
        faderAnim.SetTrigger("FadeInToNextScene");
    }

    public void FadeToNextScene()
    {
        if (goToNextScene)
        {
            goToNextScene = false;
            SceneManager.LoadScene(nextSceneName);
            nextSceneName = "";
        }
    }

    public void SetPlayerInTheNewScene()
    {
        GameManager._instance.SetPlayerPosition();
    }
}
