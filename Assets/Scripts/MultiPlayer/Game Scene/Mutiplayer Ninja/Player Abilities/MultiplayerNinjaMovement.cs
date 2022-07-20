using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TreeEditor;
using UnityEngine;

public class MultiplayerNinjaMovement : MonoBehaviour , IMovement , IPunObservable
{
    private PhotonView _photonView;

    private float _speed;
    
    private float _jumpForce;

    private Rigidbody2D _rigidBody;

    private Transform _playerTransform;
    
    private float _xAxisInput;

    private Vector3 _smoothMove;
    
    
    public void SetMovementConstraints(PhotonView photonViewv,Rigidbody2D rigidbody,Transform transform,float speed,float jumpForce)
    {
        _photonView = photonViewv;
        _speed = speed;
        _jumpForce = jumpForce;
        _rigidBody = rigidbody;
        _playerTransform = transform;
    }
    
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
    }

    private void HandleOtherMovement()
    {
         _playerTransform.position = Vector3.Lerp(_playerTransform.position,_smoothMove,Time.deltaTime*10);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
           stream.SendNext(_playerTransform);   
        }
        else if (stream.IsReading)
        {
            _smoothMove = (Vector3)stream.ReceiveNext();
        }
    }

    private void HandleMyMovement()
    {
        _xAxisInput = Input.GetAxis("Horizontal");
        
        Vector3 temp = _playerTransform.position;
        temp.x += _xAxisInput * _speed * Time.deltaTime;
        _playerTransform.position = temp;
    }
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
