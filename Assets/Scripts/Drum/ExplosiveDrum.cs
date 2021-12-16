using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDrum : MonoBehaviour
{

   [SerializeField] float force;
   
   [SerializeField] float fieldOfImpact;

   [SerializeField] LayerMask layerTiHit;


   [SerializeField] SpriteRenderer spriteRenderer;
  
   [SerializeField] Transform explosionPos;


   [SerializeField] GameObject explosionEffect;   


   void OnTriggerEnter2D( Collider2D target )
   {
       if( target.gameObject.CompareTag("PlayerProjectile"))
       {
           explode();
           print("Hit");
       }
   }

   void explode()
   {
      Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position , fieldOfImpact , layerTiHit);

      foreach(Collider2D obj in objects)
      {
        Vector2 direction = obj.transform.position - transform.position;

        obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
       
        spriteRenderer.enabled = false;

        Instantiate(explosionEffect,explosionPos.position,explosionEffect.transform.rotation);
         
        Destroy(gameObject, 5f);
      }
   }


   void OnDrawGizmos()
   {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position , fieldOfImpact );
   }
}
