using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private Rigidbody2D enteredRigidbody;
    private float enterVelocity, exitVelocity;

    
    void OnTriggerEnter2D( Collider2D target)
    {
        enteredRigidbody = target.gameObject.GetComponent<Rigidbody2D>();
        enterVelocity = enteredRigidbody.velocity.x;

        if( gameObject.name == "Red Portal")
        {
          PortalController.instance.disableCollider("Black");
          PortalController.instance.createClone("atBlack");
        }
        if( gameObject.name == "BlackPortal")
        {
          PortalController.instance.disableCollider("Red");
          PortalController.instance.createClone("atRed");
        }

    }

    void OnTriggerExit2D( Collider2D target)
    {
         if (gameObject.name != "Clone")
        {
            Destroy(target.gameObject);
            PortalController.instance.enableColliders();
        }
    }
}
