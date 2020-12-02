using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;
    public GameObject LevelSelectionMenu;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        LevelSelectionMenu.SetActive(false);
    }

    public void LevelSelectButton()
    {
        MainMenu.SetActive(false);
        LevelSelectionMenu.SetActive(true);
    }

    public void CreditsButton()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void MainMenuButton(GameObject sender)
    {
        sender.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void QuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
