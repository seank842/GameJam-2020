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
    private Text _survivorsText;
    [SerializeField]
    private Text _timerText;
    private TimeSpan _timeLeft;
    #endregion

    #region Properties 
    public Text SurvivorsText
    {
        get => _survivorsText;
        set => _survivorsText = value;
    }

    public Text TimerText
    {
        get => _timerText;
        set => _timerText = value;
    }

    public List<GameObject> Players;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Players = new List<GameObject>();
        _timeLeft = new TimeSpan(0, 0, 30);
    }

    // Update is called once per frame
    void Update()
    {
        _timeLeft -= TimeSpan.FromSeconds(Time.deltaTime);
        if(_timeLeft < TimeSpan.FromSeconds(0))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        _survivorsText.text = $"Survivors: {Players.Count}";
        _timerText.text = _timeLeft.ToString(@"m\:ss\.fff");
    }
}
