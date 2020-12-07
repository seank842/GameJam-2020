using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSceneManager : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private Text _suvivorsText;
    [SerializeField]
    private GameObject _gameTimeManager;
    [SerializeField]
    private GameObject _gameOverCanvas;
    [SerializeField]
    private List<GameObject> _players;
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
    public GameObject GameTimeManager
    {
        get => _gameTimeManager;
        set => _gameTimeManager = value;
    }
    public List<GameObject> Players
    {
        get => _players;
        set => _players = value;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        foreach (var player in Players)
        {
            player.GetComponent<SwarmPlatformerPlayer>().PlayerDestroyedEvent += GlobalSceneManager_PlayerDestroyedEvent;
        }

        PlayerChangedEvent.Invoke(this, default);

        GameTimeManager.GetComponent<GameTimeManager>().GameOverEvent += GameTimeManager_GameOverEvent;
    }

    private void GlobalSceneManager_PlayerDestroyedEvent(object sender, EventArgs e)
    {
        var senderScript = (SwarmPlatformerPlayer)sender;
        var senderGameObject = senderScript.gameObject;
        senderScript.PlayerDestroyedEvent -= GlobalSceneManager_PlayerDestroyedEvent;
        if (Players.Where(p => !p.GetInstanceID().Equals(senderGameObject.GetInstanceID())).Any())
        {
            PlayerChangedEvent?.Invoke(this, senderGameObject);
            Players.Remove(senderGameObject);
        }
        else
        {
            TriggerGameOver("Reason");
        }
    }

    private void GameTimeManager_GameOverEvent(object sender, EventArgs e)
    {
        TriggerGameOver("Reason");
    }

    public void TriggerGameOver(string reason)
    {
        foreach (var player in Players)
        {
            player.GetComponent<SwarmPlatformerPlayer>().PlayerDestroyedEvent -= GlobalSceneManager_PlayerDestroyedEvent;
        }
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

    public event EventHandler<GameObject> PlayerChangedEvent;

    // Update is called once per frame
    void Update()
    {
        SurvivorsText.text = $"Survivors: {Players.Count}";
    }

    private void OnDestroy()
    {
        GameTimeManager.GetComponent<GameTimeManager>().GameOverEvent -= GameTimeManager_GameOverEvent;
    }
}
