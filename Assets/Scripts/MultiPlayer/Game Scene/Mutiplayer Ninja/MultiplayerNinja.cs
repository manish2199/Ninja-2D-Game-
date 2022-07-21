using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerNinja : MonoBehaviour
{
    #region Serialized_Fields
    [SerializeField] private MultiplayerNinjaScpObj multiplayerNinjaScpObj;
    
    [SerializeField] private PhotonView photonView;

    [SerializeField] private Animator animator;
    #endregion

    #region Private_Variables
    private IMovement _playerMovementHandler;

    private IJUMP _playerJumpHandler;

    private CameraMove _cameraMove;
    #endregion
    
    #region Unity_Functions
    private void Awake()
    { 
         SetCamera();
 
         SetMovemenVariables();
    }
    private void FixedUpdate()
    {
        if ( _cameraMove && photonView.IsMine)
        {
            _cameraMove.cameraMovement();
        }
        
        _playerJumpHandler.HandleJump();
    }
    
    private void Update()
    {
        _playerMovementHandler.HandleMultiplayerMovement();
    }
    #endregion

    #region Initializers
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
    #endregion
}
