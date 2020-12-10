using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    #region Fields
    private int _currentScore;
    private List<int> _pickups;
    private int _playersFinished;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _playersFinishedText;
    [SerializeField]
    private Text _finalScoreText;
    #endregion

    #region Properties
    public int PlayersFinished
    {
        get => _playersFinished;
    }
    public Text ScoreText
    {
        get => _scoreText;
        set => _scoreText = value;
    }
    public Text PlayersFinishedText
    {
        get => _playersFinishedText;
        set => _playersFinishedText = value;
    }
    public Text FinalScoreText
    {
        get => _finalScoreText;
        set => _finalScoreText = value;
    }
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
        _pickups = new List<int>();
        _playersFinished = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _currentScore = (_playersFinished * 10) + _pickups.Sum();
        _scoreText.text = $"Score: {_currentScore}";
    }
    #endregion

    public void AddToPickups(int pickupValue) => _pickups.Add(pickupValue);

    public void AddToPlayersFinished(int numberOfPlayersFinsihed = 1) => _playersFinished += numberOfPlayersFinsihed;
}
