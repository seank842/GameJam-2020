using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeManager : MonoBehaviour
{
    #region Fields
    private TimeSpan _timeLeft;
    [SerializeField]
    private int _startTimeSeconds;
    [SerializeField]
    private int _startTimeMinuets;
    [SerializeField]
    private Text _timerText;
    #endregion

    #region Properties
    public int StartTimeSeconds { get; set; }
    public int StartTimeMinuets { get; set; }
    public Text TimerText
    {
        get => _timerText;
        set => _timerText = value;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _timeLeft = new TimeSpan(0, _startTimeMinuets, _startTimeSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;
        if (_timeLeft >= TimeSpan.Zero)
        {
            _timeLeft -= TimeSpan.FromSeconds(Time.unscaledDeltaTime);
        }
        else if(_timeLeft != TimeSpan.Zero)
        {
            _timeLeft = TimeSpan.Zero;
            GameOverEvent.Invoke(this, null);
        }
        TimerText.text = _timeLeft.ToString(@"m\:ss\.fff");
    }

    public event EventHandler GameOverEvent;
}
