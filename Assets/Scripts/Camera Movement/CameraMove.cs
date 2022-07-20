using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

  [HideInInspector] public Transform target;

  [SerializeField] Vector3 offsetX;

  [Range(1,10)]
  [SerializeField] float lerpRate;


  [SerializeField] float leftLimit;
  [SerializeField] float rightLimit;
  [SerializeField] float topLimit;
  [SerializeField] float bottomLimit;

  

  public void cameraMovement()
  {
    Vector3 targetPos = target.position + offsetX;

    Vector3 temp = Vector3.Lerp(transform.position , targetPos , lerpRate * Time.fixedDeltaTime);

    transform.position = temp;
  
    transform.position = new Vector3
    ( 
      Mathf.Clamp(transform.position.x,leftLimit,rightLimit),
      Mathf.Clamp(transform.position.y,bottomLimit,topLimit),
      transform.position.z 
    );
  
  }

  
  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawLine(new Vector2(leftLimit,topLimit) , new Vector2(rightLimit,topLimit) );
    Gizmos.DrawLine(new Vector2(rightLimit,topLimit) , new Vector2(rightLimit,bottomLimit) );
    Gizmos.DrawLine(new Vector2(rightLimit,bottomLimit) , new Vector2(leftLimit,bottomLimit) );
    Gizmos.DrawLine(new Vector2(leftLimit,bottomLimit) , new Vector2(leftLimit,topLimit) );
  }




}
