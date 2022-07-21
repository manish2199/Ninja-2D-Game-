using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using WebSocketSharp;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    #region Serialized_Fields
    [SerializeField] private GameObject connectedPanel;

    [SerializeField] private GameObject connectionFailPanel;

    [SerializeField] private UIHandler UIHandler;
    #endregion
    
    #region PhotonButtonFunctions
    private bool IsTextEmpty(string textField)
    {
        return textField.IsNullOrEmpty();
    }
    
    public void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.NickName = UIHandler.PlayerNameTxt;
    }
    
    public void CreateRoom()
    {
        string roomName = UIHandler.CreateRoomNameTxt;

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
        string joinRoomName = UIHandler.JoinRoomNameTxt;

        if(IsTextEmpty(joinRoomName))
        {
            return;
        }
        PhotonNetwork.JoinRoom(joinRoomName);
    }

    #endregion

    #region InputFields_Event_Functions

    public void OnIFValueChange()
    {
        if (UIHandler.PlayerNameTxt.Length > 2)
        {
            UIHandler.PhotonConnectionButton.interactable = true;
        }
        else
        {
            UIHandler.PhotonConnectionButton.interactable = false;            
        }
    }
    #endregion

    #region PhotonCallbacks
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectionFailPanel.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        if (connectionFailPanel.activeSelf)
        {
            connectionFailPanel.SetActive(false);
        }

        UIHandler.PlayerNameTMpro.text = PhotonNetwork.NickName;
        connectedPanel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed To Join Room Error : "+message);

    }

    #endregion
}
