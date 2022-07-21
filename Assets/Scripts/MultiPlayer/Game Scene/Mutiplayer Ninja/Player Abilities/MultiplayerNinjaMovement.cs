using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerNinjaMovement : MonoBehaviour, IMovement, IPunObservable
{
   [SerializeField] private PhotonView _photonView;

   [SerializeField] private Rigidbody2D _rigidBody;
   
   [SerializeField] private Transform _playerTransform;
   
   [SerializeField] private SpriteRenderer _spriteRenderer;

   private Animator _animator;
   
   private float _speed;

   private Vector3 _smoothMove;
   
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
            _photonView.RPC("FlipPlayerLeft",RpcTarget.Others);
        }
        else if (XAxisInput < 0)
        {
            _spriteRenderer.flipX = true;
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
    }
    
    [PunRPC]
    void FlipPlayerRight()
    {
        _spriteRenderer.flipX = true;        
    }
    #endregion

}



 // void playerInputHandler()
 //  {
 //    
 //    if(Input.GetButtonDown("Jump"))
 //    {
 //      jump = true;
 //    }
 //
 //    if(Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(xAxisInput) > 0) 
 //    {
 //      rigidBody.drag = 0f;
 //      rigidBody.gravityScale = 6f;
 //      ninjaSlidding();
 //    }
 //
 //     
 //    setDirection();
 //    
 //
 //    animationController();
 //
 //    playerFlip();
 //  }
 //
 //
 //  void setDirection()
 //  {
 //    if(transform.localScale.x == 0.4f)
 //    {
 //      isFacingLeft = false;
 //    }
 //    if(transform.localScale.x == -0.4f)
 //    {
 //      isFacingLeft = true;
 //    }
 //  }
 //
 //  //=================================================================================================
 //
 //
 //  void playerMovement()
 //  {
 //    
 //   
 //    
 //
 //    ninjaJump();
 //
 //    ninjaGlidding();
 //  }
 //
 //
 //  //=================================================================================================
 //
 //
 //  void ninjaJump()
 //  {
 //    if( jump && isGrounded)
 //    {
 //      // rigidBody.velocity= new Vector2(rigidBody.velocity.x,0);
 //      rigidBody.AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
 //      playerAnim.SetTrigger("Jump");
 //      
 //      jump = false;
 //    }
 //  }
 //
 //  
 //  //=================================================================================================
 //  
 //
 //  void playerFlip()
 //  {
 //    Vector3 temp = transform.localScale;
 //
 //    if(xAxisInput > 0 )
 //    {
 //      temp.x = Mathf.Abs(temp.x);
 //      ninjaDirection = temp.x;
 //    }
 //    if(xAxisInput < 0 )
 //    {
 //      temp.x = Mathf.Abs(temp.x) * (-1f);
 //      ninjaDirection = temp.x;
 //    }
 //    transform.localScale = temp;
 //  }
 //   
 //
 //  //=================================================================================================
 //
 //  
 //  
 //  void animationController()
 //  {
 //    playerAnim.SetFloat("Speed", Mathf.Abs(xAxisInput));
 //
 //    if(Input.GetKeyDown(KeyCode.O))
 //    {
 //      playerAnim.SetTrigger("Throw");
 //    }
 //
 //    if(Input.GetKeyDown(KeyCode.F) && Mathf.Abs(xAxisInput) <= 0.2f ) 
 //    {
 //      playerAnim.SetTrigger("Sword");
 //    }   
 //
 //
 //  }
