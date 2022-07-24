using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MultiplayerGameplayManager : MonoBehaviourPunCallbacks
{
    #region Serialized_Fields

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private float leftXPos;
    [SerializeField] private float rightPos;
    #endregion
    
    #region Unity_Functions
   
    void Start()
    {
        SpawnPlayer();
    }
    #endregion
    
    #region Private_Functions
    private void SpawnPlayer()
    {
        Vector2 randomPos = new Vector2(Random.Range(leftXPos, rightPos), -2.47f);
        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, playerPrefab.transform.rotation);
    }
    #endregion

    #region Photon_Callbacks

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // This player wins the match 
        GamePlayUIHandler.Instance.OnClickExitRoom();
    }
    
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("MultiplayerLobby");
 
        base.OnLeftRoom();
    }

    #endregion
}
