using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerNinjaMovement : MonoBehaviour, IMovement, IPunObservable
{
    #region Serialized_Fields
   [SerializeField] private PhotonView _photonView;

   [SerializeField] private Rigidbody2D _rigidBody;
   
   [SerializeField] private Transform _playerTransform;
   
   [SerializeField] private SpriteRenderer _spriteRenderer;
    #endregion

    #region Private_Fields
    private Animator _animator;
   
    private float _speed;

    private Vector3 _smoothMove;
    #endregion
   
    #region Getters_And_Setters
    public void SetMovementVariables(Animator animator,float speed)
    {
        _animator = animator;
        _speed = speed;
    }

    public float XAxisInput
    {
        get;
        set;
    }

    public bool IsFacingLeft
    {
        get;
        set;
    }
    
   #endregion

    #region Unity_Functions

   private void Start()
   {
       PhotonNetwork.SendRate = 30;
       PhotonNetwork.SerializationRate = 15;
   }

   #endregion
   
    #region Movement_Logic
    public void HandleMultiplayerMovement()
    {
        if (_photonView.IsMine)
        {
            // my player 
            HandleMyMovement();
        }
        else
        {
            // other player
            HandleOtherMovement();
        }
        
        AnimateMovement();
    }
    
    private void AnimateMovement()
    {
        _animator.SetFloat("Speed", Mathf.Abs(XAxisInput)); 
        _photonView.RPC("HandleMultiplayerMovementAnimation",RpcTarget.Others);
    }

    public void HandleOtherMovement()
    {
        _playerTransform.position = Vector3.Lerp(_playerTransform.position, _smoothMove, Time.deltaTime * 10);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_playerTransform.position);
        }
        else if (stream.IsReading)
        {
            _smoothMove = (Vector3)stream.ReceiveNext();
        }
    }

    private void HandleMyMovement()
    {
        XAxisInput = Input.GetAxis("Horizontal");

        FlipPlayer();

        Vector3 temp = _playerTransform.position;
        temp.x += XAxisInput * _speed * Time.deltaTime;
        _playerTransform.position = temp;
    }


    private void FlipPlayer()
    {
        if (XAxisInput > 0)
        {
            _spriteRenderer.flipX = false;
            IsFacingLeft = true;
            _photonView.RPC("FlipPlayerLeft",RpcTarget.Others);
        }
        else if (XAxisInput < 0)
        {
            _spriteRenderer.flipX = true;
            IsFacingLeft = false;
            _photonView.RPC("FlipPlayerRight",RpcTarget.Others);
        }
    }
   #endregion

    #region RPC_Calls
    [PunRPC]
    private void HandleMultiplayerMovementAnimation()
    {
        _animator.SetFloat("Speed", Mathf.Abs(XAxisInput));
    }

    [PunRPC]
    void FlipPlayerLeft()
    {
        _spriteRenderer.flipX = false;
        IsFacingLeft = true;
    }
    
    [PunRPC]
    void FlipPlayerRight()
    {
        _spriteRenderer.flipX = true;
        IsFacingLeft = false;
    }
    #endregion

}

