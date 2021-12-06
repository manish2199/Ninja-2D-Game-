using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public static PortalController instance ;

    [SerializeField] private GameObject redPortal, blackPortal;

    [SerializeField] private Transform redPortalSpawnPoint, blackPortalSpawnPoint;

    private Collider2D redPortalCollider,blackPortalCollider;

    [SerializeField] GameObject clone;

    void Awake()
    {
        makeInstance();
        redPortalCollider = redPortal.GetComponent<Collider2D>();
        blackPortalCollider = blackPortal.GetComponent<Collider2D>();
    }

    void makeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

   //==========================================================================================================================

   public void createClone( string whereToCreate)
   {
       if( whereToCreate == "atRed")
       {
            var instantiatedClone = Instantiate(clone ,redPortalSpawnPoint.position,Quaternion.identity);
            instantiatedClone.gameObject.name ="Clone";
            instantiatedClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-20f,0);
            instantiatedClone.transform.Rotate(0,0,90f);
            
       }
       else if ( whereToCreate == "atBlack")
       {
           var instantiatedClone = Instantiate(clone ,blackPortalSpawnPoint.position,Quaternion.identity);
           instantiatedClone.gameObject.name ="Clone";
            instantiatedClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-20f,0);
            instantiatedClone.transform.Rotate(0,0,90f);
       }
   }


   //==========================================================================================================================


   public void disableCollider( string colliderToDisable)
   {
       if( colliderToDisable == "Red")
       {
          redPortalCollider.enabled = false;
       }
       if( colliderToDisable == "Black")
       {
          blackPortalCollider.enabled = false;
       }
   }


   //==========================================================================================================================


   public void enableColliders()
   {
       redPortalCollider.enabled = true;
       blackPortalCollider.enabled = true;
   }

}
