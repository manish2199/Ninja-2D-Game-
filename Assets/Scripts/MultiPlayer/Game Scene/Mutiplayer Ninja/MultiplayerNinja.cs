using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerNinja : MonoBehaviour
{
    [SerializeField] private MultiplayerNinjaScpObj multiplayerNinjaScpObj;
    
    [SerializeField] private Rigidbody2D rigidbody2D;
      
    [SerializeField] private PhotonView photonView;     
    
    private IMovement _playerMovementHandler;

    private CameraMove _cameraMove;
    
    private void Awake()
    {
         SetCamera();
 
         SetMovemenVariables();
    }

    void SetMovemenVariables()
    {
        _playerMovementHandler = GetComponent<IMovement>();
       _playerMovementHandler.SetMovementConstraints(photonView,rigidbody2D,this.transform,multiplayerNinjaScpObj.speed,multiplayerNinjaScpObj.jumpForce);  
    }

    void SetCamera()
    {
        if (photonView.IsMine)
        {
            _cameraMove = GameObject.Find("Main Camera").GetComponent<CameraMove>();
            _cameraMove.target = this.transform;

        }
    }

    
    private void Update()
    {
        _playerMovementHandler.HandleMultiplayerMovement();
    }

    private void FixedUpdate()
    {
        if ( _cameraMove && photonView.IsMine)
        {
            _cameraMove.cameraMovement();
        }
    }
}
