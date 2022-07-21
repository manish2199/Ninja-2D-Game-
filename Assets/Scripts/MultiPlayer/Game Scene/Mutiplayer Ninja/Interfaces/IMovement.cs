using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public interface IMovement
{
    float XAxisInput  {  get;  set;  }

    void SetMovementVariables(Animator animator,float speed);
    
    void HandleMultiplayerMovement();
}