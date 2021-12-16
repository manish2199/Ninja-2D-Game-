using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCrystal : MonoBehaviour
{
  [SerializeField] Animator anim;

  [SerializeField] float fieldOfImpact;

  [SerializeField] LayerMask layerTiHit;


  private void OnTriggerEnter2D( Collider2D target)
  {
    if(target.gameObject.CompareTag("PlayerProjectile") || target.gameObject.CompareTag("Platform") )
    {
      anim.SetTrigger("Destroy");

      Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position , fieldOfImpact , layerTiHit);

      foreach(Collider2D obj in objects)
      {
        obj.GetComponent<RotatingPlat>().isMagicInvoke = true;         
      }

      Destroy(gameObject, 4f);
    }
  }
   void OnDrawGizmos()
   {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position , fieldOfImpact );
   }
}
