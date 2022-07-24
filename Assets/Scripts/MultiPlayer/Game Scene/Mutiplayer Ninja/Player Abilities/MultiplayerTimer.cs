using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class MultiplayerTimer : MonoBehaviour,ITimer
{

    #region Private_Fields

    private bool _isTimerActive;

    private float _currentTime;

    private float _startTime;

    private PhotonView _photonView;

    private TextMeshProUGUI _timerTextMeshProUGUI;
    
    #endregion

    #region Initializers

    public void InitializeTimer(TextMeshProUGUI timerTextMeshProUGUI, PhotonView photonView)
    {
        _timerTextMeshProUGUI = timerTextMeshProUGUI;
        _photonView = photonView;
    }
    
    #endregion
    
    #region Getters_Ans_Setters
    
    public float CurrentTime
    {
        get { return _currentTime; }
    }

    public string CurrentTimeText
    {
        get { return _timerTextMeshProUGUI.text; }
    }
    #endregion

    #region Unity_Functions

    private void Start()
    {
        _startTime = Time.time;
    }

    #endregion
    
    #region Public_Functions

    public void TimerFunction()
    {
        if (_isTimerActive)
        {
            float time = Time.time - _startTime;

            _currentTime = time; 
            
            string minutes = ((int)time / 60).ToString();
            string seconds = (time % 60).ToString("f0");

            _timerTextMeshProUGUI.text = minutes + " : " + seconds;
        }
    }
    

    public void StartTimer()
    {
        _isTimerActive = true;
    }

    public void StopTimer()
    {
        _isTimerActive = false;
    }
    

    #endregion
    
}
