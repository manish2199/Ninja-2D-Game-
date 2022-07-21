using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerNinja : MonoBehaviour
{
    [SerializeField] private MultiplayerNinjaScpObj multiplayerNinjaScpObj;
    
    [SerializeField] private PhotonView photonView;

    [SerializeField] private Animator animator;
    
    private IMovement _playerMovementHandler;

    private IJUMP _playerJumpHandler;

    private CameraMove _cameraMove;
    
    private void Awake()
    { 
         SetCamera();
 
         SetMovemenVariables();
    }



    void SetMovemenVariables()
    { 
        _playerMovementHandler = GetComponent<IMovement>();
        _playerJumpHandler = GetComponent<IJUMP>();
        _playerMovementHandler.SetMovementVariables(animator,multiplayerNinjaScpObj.speed);  
        _playerJumpHandler.SetJumpVariables(animator,multiplayerNinjaScpObj.jumpForce);
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
        
        _playerJumpHandler.HandleJump();
    }
}
