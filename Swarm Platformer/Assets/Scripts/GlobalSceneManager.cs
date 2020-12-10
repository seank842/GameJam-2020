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
    private GameObject _scoreSystem;
    [SerializeField]
    private GameObject _gameOverCanvas;
    [SerializeField]
    private GameObject _pauseMenuCanvas;
    [SerializeField]
    private List<GameObject> _players;
    [SerializeField]
    private int _requiredPlayersToFinish;
    [SerializeField]
    private string _nextLevel;
    [SerializeField]
    private GameObject _nextLevelButton;
    private bool _pause = false;
    #endregion

    #region Properties 
    public static bool GameOver { get; private set; }
    public Text SurvivorsText
    {
        get => _suvivorsText;
        set => _suvivorsText = value;
    }
    public GameObject GameTimeManager
    {
        get => _gameTimeManager;
        set => _gameTimeManager = value;
    }
    public GameObject ScoreSystem
    {
        get => _scoreSystem;
        set => _scoreSystem = value;
    }
    public GameObject GameOverCanvas
    {
        get => _gameOverCanvas;
        set => _gameOverCanvas = value;
    }
    public GameObject PauseMenuCanvas
    {
        get => _pauseMenuCanvas;
        set => _pauseMenuCanvas = value;
    }
    public List<GameObject> Players
    {
        get => _players;
        set => _players = value;
    }
    public string NextLevel
    {
        get => _nextLevel;
        set => _nextLevel = value;
    }
    public GameObject NextLevelButton
    {
        get => _nextLevelButton;
        set => _nextLevelButton = value;
    }
    public int RequiredPlayersToFinished
    {
        get => _requiredPlayersToFinish;
        set => _requiredPlayersToFinish = value;
    }
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        Players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        foreach (var player in Players)
        {
            player.GetComponent<SwarmPlatformerPlayer>().PlayerDestroyedEvent += GlobalSceneManager_PlayerDestroyedEvent;
        }

        if (string.IsNullOrEmpty(_nextLevel))
            NextLevelButton.GetComponent<Button>().interactable = false;

        PlayerChangedEvent.Invoke(this, default);

        GameTimeManager.GetComponent<GameTimeManager>().GameOverEvent += GameTimeManager_GameOverEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
            ToggelPause();
        SurvivorsText.text = $"Survivors: {Players.Count}";
    }

    private void OnDestroy()
    {
        GameTimeManager.GetComponent<GameTimeManager>().GameOverEvent -= GameTimeManager_GameOverEvent;
    }
    #endregion

    private void TriggerGameOver()
    {
        foreach (var player in Players)
        {
            player.GetComponent<SwarmPlatformerPlayer>().PlayerDestroyedEvent -= GlobalSceneManager_PlayerDestroyedEvent;
        }
        GameOver = true;
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    private void TriggerVictory()
    {

        Time.timeScale = 0;
    }

    public void ToggelPause()
    {
        _pause = !_pause;
        Time.timeScale = _pause ? 0 : 1;
        PauseMenuCanvas.SetActive(_pause);
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

    public void GoToNextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_nextLevel);
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

    #region Events
    public event EventHandler<GameObject> PlayerChangedEvent;
    #endregion

    #region Event Calls
    private void GlobalSceneManager_PlayerDestroyedEvent(object sender, bool playerFinished)
    {
        if(playerFinished)
            ScoreSystem.GetComponent<ScoreSystem>().AddToPlayersFinished();

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
            if (ScoreSystem.GetComponent<ScoreSystem>().PlayersFinished == _requiredPlayersToFinish)
                TriggerGameOver();
            else
                TriggerVictory();
        }
    }

    private void GameTimeManager_GameOverEvent(object sender, EventArgs e)
    {
        TriggerGameOver();
    }
    #endregion
}
