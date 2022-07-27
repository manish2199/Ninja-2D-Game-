using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class MainMenuUIHandler : MonoBehaviour
{
   #region Serialized_Fields
   [SerializeField] private TMP_InputField createRoomNameIF;
   
   [SerializeField] private TMP_InputField joinRoomNameIF;
   
   [SerializeField] private TMP_InputField playerNameIF;

   [SerializeField] private Button connectToPhotonButton;
   
   [SerializeField] private TextMeshProUGUI connectedPanelPlayerName ;

   [SerializeField] private TextMeshProUGUI connectingTextMeshProUGUI;
   
   [SerializeField] private TextMeshProUGUI roomNameTextMeshProUGUI;

   [SerializeField] private GameObject startGameButton;
   
   
   #endregion
   
   #region Getters_And_Setters
   public string CreateRoomNameIFTxt
   {
      get
      {
         return createRoomNameIF.text;
      }

   }
   
   public string JoinRoomNameIFTxt
   {
      get
      {
         return joinRoomNameIF.text;
      }
   }
   public string PlayerNameIFTxt
   {
      get
      {
         return playerNameIF.text;
      }
   
      set
      {
         playerNameIF.text = value;
      }
   }
  
   #endregion

   #region Public_Funtions
   public void EnableConnectingText()
   {
      connectingTextMeshProUGUI.gameObject.SetActive(true);
   }

   public void HandleOnClickLeaveRoom()
   {
      createRoomNameIF.text = "";
      joinRoomNameIF.text = "";
   }


   public void ConnectionButtonInteraction()
   {
      if (PlayerNameIFTxt.Length > 2)
      {
         connectToPhotonButton.interactable = true;
      }
      else
      {
         connectToPhotonButton.interactable = false;            
      }
   }
   
   public void ResetPlayerNameIF()
   {
      PlayerNameIFTxt = "";
   }

   public void ResetPlayerNameTMPro()
   {
      connectedPanelPlayerName.text = "";
   }


   public void HandleStartButton(bool isEnable)
   {
      startGameButton.SetActive(isEnable);
   }

   public void HandleOnJoinLobby(string NickName)
   {
      connectingTextMeshProUGUI.gameObject.SetActive(false);
      connectedPanelPlayerName.text = NickName;
   }

   public void HandleRoomName(string roomName)
   {
      roomNameTextMeshProUGUI.text = roomName;
   }

   public void ResetRoomName()
   {
      roomNameTextMeshProUGUI.text = "";
   }


   #endregion
}
