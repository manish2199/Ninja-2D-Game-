using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCrystal : MonoBehaviour
{
  [SerializeField] Animator anim;


  private void OnTriggerEnter2D( Collider2D target)
  {
    if(target.gameObject.CompareTag("PlayerProjectile") || target.gameObject.CompareTag("Platform"))
    {
        anim.SetTrigger("Destroy");

        Destroy(gameObject, 4f);
    }
    if(target.gameObject.CompareTag("Player"))
    {
        Destroy(gameObject);
        // print("Collected By Player");
    }
  }

}
