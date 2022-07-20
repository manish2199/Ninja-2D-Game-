using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerGameplayManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private float leftXPos;
    [SerializeField] private float rightPos;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Vector2 randomPos = new Vector2(Random.Range(leftXPos, rightPos), -2.47f);
        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, playerPrefab.transform.rotation);
    }
}
