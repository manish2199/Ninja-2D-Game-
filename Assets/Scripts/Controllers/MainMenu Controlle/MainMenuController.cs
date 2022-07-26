using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class MainMenuController : MonoBehaviour
{
  [SerializeField] GameObject levelSelector;

  [SerializeField] GameObject mainMenuPanel;

  public void PlaySinglePlayer()
  {
    levelSelector.SetActive(true);
    mainMenuPanel.SetActive(false);
  }


  public void PlayMultiplayer()
  {
    SceneManager.LoadScene("MultiplayerLobby");
  }

}
