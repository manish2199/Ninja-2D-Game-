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
 
    [SerializeField] private SpriteRenderer spriteRenderer;     

    [SerializeField] private Animator animator;     
    
    [SerializeField]private AnimationController animationController;
    
    [SerializeField]private Transform transform;

    private IMovement _playerMovementHandler;

    private CameraMove _cameraMove;
    
    private void Awake()
    {
         SetAnimator();
        
         SetCamera();
 
         SetMovemenVariables();
    }

    private void SetAnimator()
    {
        animationController.SetAnimatorVariables(animator,photonView);
    }

    void SetMovemenVariables()
    {
        _playerMovementHandler = GetComponent<IMovement>();
       _playerMovementHandler.SetMovementConstraints(photonView,rigidbody2D,transform,multiplayerNinjaScpObj.speed,multiplayerNinjaScpObj.jumpForce,spriteRenderer);  
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
        
        animationController.HandleAnimation(_playerMovementHandler.XAxisInput);

    }

    private void FixedUpdate()
    {
        if ( _cameraMove != null && photonView.IsMine)
        {
            _cameraMove.cameraMovement();
        }
    }
}
