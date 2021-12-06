using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
   //Input 
   float xAxisInput;


  [Header("Player Movement")]
  [SerializeField] Rigidbody2D rigidBody;
  [SerializeField] float speed;
  [SerializeField] Vector3 colliderOffset;
  [SerializeField] float groundLength;
  [SerializeField] LayerMask groundLayer;
  bool isGrounded;
  bool jump;
  [SerializeField] float gravity = 1;
  [SerializeField] float gravityMultiplyer = 5;
  [SerializeField] float jumpForce;
  bool isSliding;
  [SerializeField] float slidingSpeed;
  [SerializeField] BoxCollider2D defualtCollider;
  [SerializeField] BoxCollider2D slidingCollider;
  

  [Header("\nShooting\n")]
  [SerializeField] GameObject projectile;
  [SerializeField] Transform shootingPos;
  bool isFacingLeft;

  [Header("\nAnimation Controller\n")]
  [SerializeField] Animator playerAnim;
   
  [Header("\nGlidding Setting\n")]
  bool gliderCollider;
  bool isGlidding;
  [SerializeField] Transform gliderPos;
  [SerializeField] float gliderOpeneingDistance;
  float ninjaDirection;
  [SerializeField] float gliddingForce;

  


  void Update()
  {
    setRayCasts();

    playerInputHandler();

  }

  void FixedUpdate()
  {
    ModifyGravity();

    playerMovement();
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
    if( Input.GetKey(KeyCode.G) && !gliderCollider )
    {
      isGlidding = true;
      playerAnim.SetBool("Glide",true);
      rigidBody.drag = 9f;
    }
    if( Input.GetKeyUp(KeyCode.G) || gliderCollider)
    {
      isGlidding = false;
      playerAnim.SetBool("Glide",false);
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
        rigidBody.gravityScale = 0f;
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

    if(Input.GetKeyDown(KeyCode.LeftShift) && xAxisInput != 0) 
    {
      rigidBody.drag = 0f;
      rigidBody.gravityScale = 6f;
      ninjaSlidding();
    }

    if(transform.localScale.x == 0.4f)
    {
      isFacingLeft = false;
    }
    if(transform.localScale.x == -0.4f)
    {
      isFacingLeft = true;
    }
 
    animationController();

    playerFlip();
  }

  //=================================================================================================
 

  void playerMovement()
  {
    if(!isSliding)
    {
      rigidBody.velocity = new Vector3( xAxisInput * speed , rigidBody.velocity.y);
    }

    ninjaJump();

    ninjaGlidding();
  }


  //=================================================================================================


  void ninjaJump()
  {
    if(jump && isGrounded)
    {
      GetComponent<Rigidbody2D>().velocity= new Vector2(GetComponent<Rigidbody2D>().velocity.x,0);
      GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
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


    if(Input.GetMouseButtonDown(0))
    {
      playerAnim.SetTrigger("Throw");
      // shootProjectile();
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

    // rigidBody.drag = 0f;
    // rigidBody.gravityScale = 1f;
  
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
    yield return new WaitForSeconds(0.2f);
    
    rigidBody.drag = 0.6f;
    rigidBody.gravityScale = 0f;
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
}
