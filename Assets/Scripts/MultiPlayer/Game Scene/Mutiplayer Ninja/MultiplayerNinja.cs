using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using UnityEngine;

public class MultiplayerNinja : MonoBehaviour
{
    #region Serialized_Fields
    [SerializeField] private MultiplayerNinjaScpObj multiplayerNinjaScpObj;
    
    [SerializeField] private PhotonView photonView;

    [SerializeField] private Animator animator;

    [SerializeField] private TextMeshProUGUI playerNameText; 
    #endregion

    #region Private_Variables
    private IMovement _playerMovementHandler;

    private IJUMP _playerJumpHandler;

    private IShoot _playerShootHandler;

    private CameraMove _cameraMove;
    
    #endregion
   
    #region Initializers
    void InitializeMovemenVariables()
    { 
        _playerMovementHandler = GetComponent<IMovement>();
        _playerJumpHandler = GetComponent<IJUMP>();
        _playerMovementHandler.SetMovementVariables(animator,multiplayerNinjaScpObj.speed);  
        _playerJumpHandler.SetJumpVariables(animator,multiplayerNinjaScpObj.jumpForce);
    }

    void InitializeShootVariables()
    {
        _playerShootHandler = GetComponent<IShoot>();
        _playerShootHandler.SetShootVariables(photonView,animator);
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

    #region Unity_Functions
    private void Awake()
    { 
         SetCamera();
 
         InitializeMovemenVariables();
         
         InitializeShootVariables();
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            playerNameText.text = PhotonNetwork.NickName;
        }
        else
        {
            playerNameText.text = photonView.Owner.NickName;
        }
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
        
        _playerShootHandler.HandlePlayerShoot(_playerMovementHandler.IsFacingLeft);
    }
    #endregion

    #region Public_Funtions

    public void ShootProjectile()
    {
        _playerShootHandler.ShootProjectile();
    }
    

    #endregion
}

