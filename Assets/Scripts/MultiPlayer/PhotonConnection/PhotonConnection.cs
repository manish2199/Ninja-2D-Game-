using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.PlayerLoop;
using WebSocketSharp;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    #region Serialized_Fields
    
    [SerializeField] private GameObject mainMenuPanel;
    
    [SerializeField] private GameObject connectedPanel;

    [SerializeField] private GameObject connectionFailPanel;

    [SerializeField] private GameObject inRoomPanel;

    [SerializeField] private Transform playerListingTransform;

    [SerializeField] private PlayerItem playerItemPrefab;

    [SerializeField] private UIHandler UIHandler;
    #endregion
    
    #region Private_Fields

    private List<PlayerItem> _playerItems = new List<PlayerItem>();

    #endregion
    
    #region PhotonButtonFunctions
    private bool IsTextEmpty(string textField)
    {
        return textField.IsNullOrEmpty();
    }
    
    public void ConnectToPhoton()
    {
        UIHandler.ConnectingTextTMPro.gameObject.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = UIHandler.PlayerNameIFTxt;
    }
    
    public void CreateRoom()
    {
        string roomName = UIHandler.CreateRoomNameIFTxt;

        if(IsTextEmpty(roomName))
        {
            return;
        }
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomName,roomOptions, null);
    }
    
    
    public void JoinRoom()
    {
        string joinRoomName = UIHandler.JoinRoomNameIFTxt;

        if(IsTextEmpty(joinRoomName))
        {
            return;
        }
        PhotonNetwork.JoinRoom(joinRoomName);
    }


    public void DisconnectPhoton()
    {
        StartCoroutine(DisconnectPhotonRoutine());
    }


    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        UIHandler.CreateRoomNameIFTxt = "";
        UIHandler.JoinRoomNameIFTxt = "";
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("MultiplayerGameScene");
    }

    #endregion

    #region InputFields_Event_Functions

    public void OnIFValueChange()
    {
        if (UIHandler.PlayerNameIFTxt.Length > 2)
        {
            UIHandler.PhotonConnectionButton.interactable = true;
        }
        else
        {
            UIHandler.PhotonConnectionButton.interactable = false;            
        }
    }

    
    #endregion

    #region Private_Functions

    private IEnumerator OnDissconnectedRoutine()
    {
        if (connectedPanel.activeSelf)
        {
            connectedPanel.SetActive(false);
        }
        

        connectionFailPanel.SetActive(true);
        UIHandler.PlayerNameIFTxt = "";
        
        yield return new WaitForSeconds(1f);
        
        mainMenuPanel.SetActive(true);
        connectionFailPanel.SetActive(false);
    }


    private IEnumerator DisconnectPhotonRoutine()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
           yield return null;
        }
        UIHandler.PlayerNameTMpro.text = "";
    }



    private void UpdatePlayerItemList()
    {
        foreach (PlayerItem item in _playerItems)
        {
            Destroy(item.gameObject);
        }
        _playerItems.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int,Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerListingTransform);
            newPlayerItem.SetPlayerName(player.Value);
            _playerItems.Add(newPlayerItem);
        }

    }


    private void CheckForStartGameButton()
    {
        if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            UIHandler.StartGameButton.SetActive(true);
        }
        else if(!PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            UIHandler.StartGameButton.SetActive(false);            
        }
    }
    

    #endregion

    #region Unity_Funtions

    private void Update()
    {
       CheckForStartGameButton();
    }

    #endregion
    
    #region PhotonCallbacks
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        StartCoroutine(OnDissconnectedRoutine());
    }

    public override void OnJoinedLobby()
    {
        if (connectionFailPanel.activeSelf)
        {
            connectionFailPanel.SetActive(false);
        }

        mainMenuPanel.SetActive(false);
        UIHandler.ConnectingTextTMPro.gameObject.SetActive(false);
        UIHandler.PlayerNameTMpro.text = PhotonNetwork.NickName;
        connectedPanel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        
        connectedPanel.SetActive(false);
        inRoomPanel.SetActive(true);
        UIHandler.RoomName = PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerItemList();
    }

    public override void OnLeftRoom()
    {
        UIHandler.RoomName = "";
        connectedPanel.SetActive(true);
        inRoomPanel.SetActive(false);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed To Join Room Error : "+message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       UpdatePlayerItemList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerItemList();      
    }

    #endregion
}
