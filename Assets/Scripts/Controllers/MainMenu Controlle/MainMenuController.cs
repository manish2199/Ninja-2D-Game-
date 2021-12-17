using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    
  [SerializeField] GameObject levelSelector;

  [SerializeField] GameObject mainMenuPanel;
  




  public void PlayButton()
  {
    levelSelector.SetActive(true);
    mainMenuPanel.SetActive(false);
  }




}
