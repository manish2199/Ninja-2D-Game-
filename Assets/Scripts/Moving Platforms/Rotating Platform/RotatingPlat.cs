using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlat : MonoBehaviour
{
  [SerializeField] private float rotationalSpeed; 

  // [SerializeField] Transform rotateForm;
  // [SerializeField] Transform rotateTo;


  Quaternion targetAngle = Quaternion.Euler(0,0,-90);
 
  public bool isMagicInvoke;  

  public bool collidedWithPlayer; 
 
 
  void Update()
  {
    if(isMagicInvoke)
    {
      slowRotate(); 
    }

    // print(Time.time);
  }


  void slowRotate()
  { 
    // yield return new WaitForSeconds(0.5f);

    // transform.rotation = Quaternion.Lerp(rotateForm.rotation,rotateTo.rotation, Time.time * rotationalSpeed); 

    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,targetAngle,rotationalSpeed);
  }

}
