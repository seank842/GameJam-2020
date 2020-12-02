using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    #region Fields
    private int _currentScore;
    [SerializeField]
    private Text _scoreText;
    #endregion

    #region Properties
    public Text ScoreText
    {
        get => _scoreText;
        set => _scoreText = value;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;

    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = $"Score: {_currentScore}";
    }
}
