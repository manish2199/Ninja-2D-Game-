using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiplayerGameplayManager : MonoBehaviourPunCallbacks
{
    #region Public_Fields

    public static MultiplayerGameplayManager Instance;

    #endregion
    
    #region Serialized_Fields

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private float leftXPos;
    [SerializeField] private float rightPos;
    #endregion

    #region Private_Field

    private List<MultiplayerNinja> _players;

    #endregion

    #region Initializers
    private void InitializePlayerList()
    {
         _players = new List<MultiplayerNinja>();
    }

    private void InitializeInstance()
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
        InitializePlayerList();
        
        InitializeInstance();
    }

    void Start()
    {
        SpawnPlayer();
    }
    #endregion

    #region Public_Funtions

    public void ShowGameWinner(string winnerNameText)
    {
       GamePlayUIHandler.Instance.ShowWinner(winnerNameText);
    }
      
    

    #endregion
    
    #region Private_Functions
    private void SpawnPlayer()
    { 
        Vector2 randomPos = new Vector2(Random.Range(leftXPos, rightPos), -2.47f);
       GameObject gameObject = PhotonNetwork.Instantiate(playerPrefab.name, randomPos, playerPrefab.transform.rotation);
       _players.Add(gameObject.GetComponent<MultiplayerNinja>());
    }
    #endregion

    #region Photon_Callbacks

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // other player left match 
        GamePlayUIHandler.Instance.ShowOtherPlayerLeftUI(otherPlayer.NickName);
    }
    
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("MultiplayerLobby");
    }

    #endregion
}
