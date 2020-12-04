using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSceneManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Text _suvivorsText;
    [SerializeField]
    private GameTimeManager _gameTimeManager;
    [SerializeField]
    private GameObject _gameOverCanvas;
    [SerializeField]
    private static List<GameObject> _players;
    #endregion

    #region Properties 
    public static bool GameOver { get; private set; }
    public Text SurvivorsText
    {
        get => _suvivorsText;
        set => _suvivorsText = value;
    }
    public GameObject GameOverCanvas
    {
        get => _gameOverCanvas;
        set => _gameOverCanvas = value;
    }
    public GameTimeManager GameTimeManager
    {
        get => _gameTimeManager;
        set => _gameTimeManager = value;
    }
    public static List<GameObject> Players
    {
        get => _players;
        set => _players = value;
    }
    public GameObject[] PlayersArray { get; set; } // FindGameObjectWithTag returns an array not a list so this is needed
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Players = new List<GameObject>();
        PlayersArray = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in PlayersArray)
        {
            Players.Add(player);
        }

        GameTimeManager.GameOverEvent += GameTimeManager_GameOverEvent;
    }

    private void GameTimeManager_GameOverEvent(object sender, EventArgs e)
    {
        GameOver = true;
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void RetryScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void GotToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1;
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        SurvivorsText.text = $"Survivors: {Players.Count}";
    }
}
