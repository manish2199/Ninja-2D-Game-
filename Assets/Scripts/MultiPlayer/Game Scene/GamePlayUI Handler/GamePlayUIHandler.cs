using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GamePlayUIHandler : MonoBehaviour
{
    #region Public_Fields

    public static GamePlayUIHandler Instance;
    
    #endregion
 
    #region SerializedFields

    [SerializeField] private GameObject pausePanel;
    
    [SerializeField] private GameObject pauseButton;

    #endregion

    #region Private_Fields


    #endregion

    #region Initializers

    private void MakeStaticInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    

    #endregion

    #region Unity_Functions

    private void Awake()
    {
        MakeStaticInstance();
    }

    #endregion
    
    #region Public_Functions

    public void OnClickPauseButton()
    {
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void OnClickResume()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void OnClickExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    #region Private_Funtions

    
    

    #endregion
}
