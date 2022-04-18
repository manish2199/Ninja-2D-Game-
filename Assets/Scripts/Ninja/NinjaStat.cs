using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStat : MonoBehaviour
{
   [SerializeField]private int lives; 

   public int Lives
   {
      get 
      {
          return lives;
      }
   }

   void OnTriggerEnter2D( Collider2D target)
   {
      if(target.gameObject.CompareTag("LowerBoundary") )
      {
        GamePlayController.instance.respawnToNearestCheckPoint(); 
      //   lives --;
      }
   } 
}
