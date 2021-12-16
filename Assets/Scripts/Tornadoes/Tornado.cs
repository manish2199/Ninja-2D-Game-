using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
  [SerializeField] AreaEffector2D areaEffector;

  void OnEnable()
  {
     PlayerController.hitTornadoCollider += hit_TornadoCollider;
  } 

  void OnDisable()
  {
    PlayerController.hitTornadoCollider -= hit_TornadoCollider;
  }
   
    

   void hit_TornadoCollider(bool isGlide)
   {
    
    if ( isGlide )
    { 
      areaEffector.enabled = true;
    }
    else
    {
      areaEffector.enabled = false;
    } 
   
   }


}
