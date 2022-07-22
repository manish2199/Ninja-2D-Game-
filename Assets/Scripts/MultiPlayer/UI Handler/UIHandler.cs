using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class UIHandler : MonoBehaviour
{
   #region Serialized_Fields
   [SerializeField] private TMP_InputField createRoomNameIF;
   
   [SerializeField] private TMP_InputField joinRoomNameIF;
   
   [SerializeField] private TMP_InputField playerNameIF;

   [SerializeField] private Button connectToPhotonButton;
   
   [SerializeField] private TextMeshProUGUI connectedPanelPlayerName ;

   [SerializeField] private TextMeshProUGUI roomName;

   [SerializeField] private GameObject startGameButton;
   
   #endregion
   
   #region Getters_And_Setters
   public string CreateRoomNameIFTxt
   {
      get
      {
         return createRoomNameIF.text;
      }

      set
      {
         createRoomNameIF.text = value;
      }
   }
   
   public string JoinRoomNameIFTxt
   {
      get
      {
         return joinRoomNameIF.text;
      }

      set
      {
         joinRoomNameIF.text = value;
      }
   }

   public Button PhotonConnectionButton
   {
      get
      {
         return connectToPhotonButton;
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
   
   public TextMeshProUGUI PlayerNameTMpro
   {
      get
      {
         return connectedPanelPlayerName;
      }
   }
   
   
   public string RoomName
   {
      get
      {
         return roomName.text;
      }

      set
      {
         roomName.text = value;
      }
   }

   public GameObject StartGameButton
   {
      get
      {
         return startGameButton;
      }
   }
   
   #endregion
}
