using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class GamePlayUIHandler : MonoBehaviour
{
    #region Public_Fields

    public static GamePlayUIHandler Instance;

    #endregion

    #region SerializedFields

    [SerializeField] private GameObject pausePanel;

    [SerializeField] private GameObject pauseButton;

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI winnerTextMeshProUGUI;

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

    public void ShowWinner(string winnerText)
    {
        winnerTextMeshProUGUI.text = " Winner is " + winnerText;
        gameOverPanel.SetActive(true);
    }

    public void ShowOtherPlayerLeftUI(string otherPlayer)
    {
        string winnerText = " Winner is " + otherPlayer;
        if (winnerText == winnerTextMeshProUGUI.text)
        {
             // means winner left the room so dont show left room text
             OnClickExitRoom();
             return;
        }
        else
        {
            winnerTextMeshProUGUI.text = otherPlayer + " Has Left The Room You won!!";       
            gameOverPanel.SetActive(true);
            StartCoroutine(ExitGameCoroutine());
        }
    }

    public IEnumerator ExitGameCoroutine()
    {
        yield return new WaitForSeconds(5f);

        DeactivateGameOver();
        OnClickExitRoom();
    }


    public void DeactivateGameOver()
    {
        gameOverPanel.SetActive(false);
        winnerTextMeshProUGUI.text = "";
    }

    #endregion

}
