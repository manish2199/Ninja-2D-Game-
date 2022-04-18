using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
  //Input 
  float xAxisInput;
  public static event Action<bool> hitTornadoCollider = delegate { };


  [Header("\nPlayer Movement\n")]
  bool isGrounded;
  bool jump;
  bool isSliding;
  [SerializeField] Rigidbody2D rigidBody;
  [SerializeField] BoxCollider2D defualtCollider;
  [SerializeField] BoxCollider2D slidingCollider;
  [SerializeField] LayerMask groundLayer;
  [SerializeField] Vector3 colliderOffset;
  [SerializeField] float speed;
  [SerializeField] float groundLength;
  [SerializeField] float gravity = 1;
  [SerializeField] float gravityMultiplyer = 5;
  [SerializeField] float jumpForce;
  [SerializeField] float slidingSpeed;


  [Header("\nShooting\n")]
  bool isFacingLeft;
  [SerializeField] GameObject projectile;
  [SerializeField] Transform shootingPos;

  [Header("\nAnimation Controller\n")]
  [SerializeField] Animator playerAnim;
   
  [Header("\nGlidding Setting\n")]
  bool gliderCollider;
  float ninjaDirection;
  [HideInInspector]public bool isGlidding;
  [SerializeField] Transform gliderPos;
  [SerializeField] float gliderOpeneingDistance;
  [SerializeField] float gliddingForce;

  void Update()
  {
    setRayCasts();

    playerInputHandler();
  }


  void FixedUpdate()
  {
   playerMovement();

    ModifyGravity();
  }
  
  //=================================================================================================


  void setRayCasts()
  {
    isGrounded =Physics2D.Raycast(transform.position + colliderOffset,Vector2.down,groundLength,groundLayer) || Physics2D.Raycast(transform.position-colliderOffset,Vector2.down,groundLength,groundLayer);
    
    gliderCollider = Physics2D.Raycast(gliderPos.position, Vector3.down,gliderOpeneingDistance,groundLayer);

  }


  //=================================================================================================



  void ninjaGlidding()
  {
    if(Input.GetKeyDown(KeyCode.M) && !gliderCollider )
    {
      
      // print("Glide");
      isGlidding = true;
      playerAnim.SetBool("Glide",true);
      rigidBody.drag = 9f;
      hitTornadoCollider(true);
    }
    if( Input.GetKeyUp(KeyCode.M) || gliderCollider)
    {
      // areaEffector.enabled = false;
      isGlidding = false;
      playerAnim.SetBool("Glide",false);
      hitTornadoCollider(false);
    }
  }

  //=================================================================================================


  void ModifyGravity()
  {
    if( !isSliding)
    {
     if( !isGrounded )
     {
       if(!isGlidding)
       {
        rigidBody.gravityScale = gravity; 
        rigidBody.drag = 0.6f;
        if ( rigidBody.velocity.y < 0 )
        {
          rigidBody.gravityScale = gravity * gravityMultiplyer;
        }
        if ( rigidBody.velocity.y > 0 && !Input.GetButtonDown("Jump") )
        {
          rigidBody.gravityScale = gravity * ( gravityMultiplyer / 2);
        }
      }
     }
     else
     {
        playerAnim.SetBool("Glide",false);
        rigidBody.gravityScale = 0.5f;
    }
    }
  }



  //=================================================================================================
 



  void playerInputHandler()
  {

    xAxisInput = Input.GetAxis("Horizontal");
    
    if(Input.GetButtonDown("Jump"))
    {
      jump = true;
    }

    if(Input.GetKeyDown(KeyCode.LeftShift) && Mathf.Abs(xAxisInput) > 0) 
    {
      rigidBody.drag = 0f;
      rigidBody.gravityScale = 6f;
      ninjaSlidding();
    }
 
     
    setDirection();
    
 
    animationController();

    playerFlip();
  }


  void setDirection()
  {
    if(transform.localScale.x == 0.4f)
    {
      isFacingLeft = false;
    }
    if(transform.localScale.x == -0.4f)
    {
      isFacingLeft = true;
    }
  }

  //=================================================================================================
 

  void playerMovement()
  {
    
    Vector3 temp = transform.position;
    temp.x += xAxisInput * speed * Time.fixedDeltaTime;
    transform.position = temp;
    

    ninjaJump();

    ninjaGlidding();
  }


  //=================================================================================================


  void ninjaJump()
  {
    if( jump && isGrounded)
    {
      // rigidBody.velocity= new Vector2(rigidBody.velocity.x,0);
      rigidBody.AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
      playerAnim.SetTrigger("Jump");
      
      jump = false;
    }
  }

  
  //=================================================================================================
  

  void playerFlip()
  {
    Vector3 temp = transform.localScale;

    if(xAxisInput > 0 )
    {
      temp.x = Mathf.Abs(temp.x);
      ninjaDirection = temp.x;
    }
    if(xAxisInput < 0 )
    {
      temp.x = Mathf.Abs(temp.x) * (-1f);
      ninjaDirection = temp.x;
    }
    transform.localScale = temp;
  }
   

  //=================================================================================================

  
  
  void animationController()
  {
    playerAnim.SetFloat("Speed", Mathf.Abs(xAxisInput));
 
    if(Input.GetKeyDown(KeyCode.O))
    {
      playerAnim.SetTrigger("Throw");
    }

    if(Input.GetKeyDown(KeyCode.F) && Mathf.Abs(xAxisInput) <= 0.2f ) 
    {
      playerAnim.SetTrigger("Sword");
    }   


  }

  //=================================================================================================
 
  void shootProjectile()
  {
    GameObject Projectile = Instantiate(projectile);
    Projectile.GetComponent<Projectile>().shootProjectile(isFacingLeft);
    Projectile.transform.position = shootingPos.position;
  }


  //=================================================================================================

  void ninjaSlidding()
  {
    
    isSliding = true;

    playerAnim.SetTrigger("Slide");

    defualtCollider.enabled = false;
    slidingCollider.enabled = true;
  
    if ( !isFacingLeft)
    {
      rigidBody.AddForce(Vector2.right * slidingSpeed);
    }
    else 
    {
      rigidBody.AddForce(Vector2.left * slidingSpeed);
    }

    StartCoroutine(stopSlide());
  }  

  IEnumerator stopSlide()
  {
    yield return new WaitForSeconds(0.9f);
    
    rigidBody.drag = 0.6f;
    rigidBody.gravityScale = 0.5f;
    isSliding = false;
    defualtCollider.enabled = true;
    slidingCollider.enabled = false;

  }


  //=================================================================================================


 void OnDrawGizmos()
  {
    Gizmos.color =  Color.red;
    Gizmos.DrawLine(transform.position + colliderOffset , transform.position + colliderOffset + Vector3.down * groundLength);
    Gizmos.DrawLine(transform.position - colliderOffset , transform.position - colliderOffset + Vector3.down * groundLength);
    

    // For Glider 
    Gizmos.color = Color.blue;
    Gizmos.DrawLine(gliderPos.position , gliderPos.position + Vector3.down * gliderOpeneingDistance);
   }

  //=================================================================================================
   

  

}
