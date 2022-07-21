using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public interface IShoot
{
    void SetShootVariables(PhotonView photonView,Animator animator);

    void ShootProjectile();
    void HandlePlayerShoot(bool isFacingLeft);
}


