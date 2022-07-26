using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerJump : MonoBehaviour , IJUMP
{
    #region Serialized_Fields
    
    [SerializeField] private Transform _playerTransform;

    [SerializeField] Vector3 _colliderOffset;
    
    [SerializeField] float _groundLength;
    
    [SerializeField] LayerMask _groundLayer;
    
    [SerializeField] private PhotonView photonView;
    
    [SerializeField] private Rigidbody2D _rigidbody2D;
    #endregion

    #region Private_Fields
    private Animator _animator;
    
    private bool _IsGrounded = false;

    private float _jumpForce;
    #endregion
    
    #region Getters_And_Setters
    public bool CanJump
    {
        get;

        set;
    }
    
    public void SetJumpVariables(Animator animator,float jumpForce)
    {
       _jumpForce = jumpForce;
       _animator = animator;
    }
    #endregion

    #region Unity_Funtions
    private void Start()
    {
       _rigidbody2D = GetComponent<Rigidbody2D>();
       PhotonNetwork.SendRate = 30;
       PhotonNetwork.SerializationRate = 15;
    }
    
    private void Update()
    {
        if(photonView.IsMine)
        {
            SetRayCast();

            HandlePlayerInputs();
        }
    }
    #endregion
    
    #region Multiplayer_Jump_Logic
    private void SetRayCast()
    {
        _IsGrounded =Physics2D.Raycast(_playerTransform.position + _colliderOffset,Vector2.down,_groundLength,_groundLayer) || Physics2D.Raycast(transform.position-_colliderOffset,Vector2.down,_groundLength,_groundLayer);
    }

    private void HandlePlayerInputs()
    {
        if (Input.GetButtonDown("Jump") && _IsGrounded)
        {
            CanJump = true; 
        }
    }
    
    public void HandleJump()
    {
        if (photonView.IsMine && CanJump)
        {
            // jump
            ApplyForce();
            AnimateJump();
            photonView.RPC("HandleOtherPlayerJump",RpcTarget.Others);
            
            CanJump = false;
        }
    }
    
    void ApplyForce()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce , ForceMode2D.Impulse );
    }
    
    void AnimateJump()
    {
        if (photonView.IsMine)
        {
            _animator.SetTrigger("Jump");
            photonView.RPC("HandleMultiplayerJumpAnimation", RpcTarget.Others);
        }
    }
    #endregion


    #region RPC_Calls

    [PunRPC]
    private void HandleOtherPlayerJump()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce , ForceMode2D.Impulse );
    }
    
    [PunRPC]
    private void HandleMultiplayerJumpAnimation()
    {
        _animator.SetTrigger("Jump");           
    }
    #endregion


    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawLine(transform.position + _colliderOffset,transform.position + _colliderOffset + Vector3.down * _groundLength);
    //     Gizmos.DrawLine(transform.position - _colliderOffset,
    //         transform.position - _colliderOffset + Vector3.down * _groundLength);
    // }
    

}
