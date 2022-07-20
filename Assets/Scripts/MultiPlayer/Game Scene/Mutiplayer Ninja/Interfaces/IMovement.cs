using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public interface IMovement
{
    void SetMovementConstraints(PhotonView photonViewv,Rigidbody2D rigidbody,Transform transform,float speed,float jumpForce);
    void HandleMultiplayerMovement();
}