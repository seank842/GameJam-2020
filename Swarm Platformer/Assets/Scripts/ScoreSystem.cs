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

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        _currentScore = 0;
        _pickups = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = $"Score: {_pickups.Sum()}";
    }
    #endregion

    public void AddToPickups(int pickupValue) => _pickups.Add(pickupValue);
}
