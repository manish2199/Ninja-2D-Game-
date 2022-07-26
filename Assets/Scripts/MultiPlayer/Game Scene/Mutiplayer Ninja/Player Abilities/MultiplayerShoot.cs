using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerShoot : MonoBehaviour , IShoot
{
    #region PrivateFields

    private bool _canShoot = false;

    private bool _shootRight = false;
    
    private Animator _animator;
    
    private PhotonView _photonView;

    #endregion

    #region SerializedFields

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform leftSpawnPosition;
    [SerializeField] private Transform rightSpawnPosition;

    
    #endregion

    #region Public_Functions

    public void SetShootVariables(PhotonView photonView,Animator animator)
    {
        _animator = animator;
        _photonView = photonView;
    }

    public void HandlePlayerShoot(bool isFacingLeft)
    {
        if ( _photonView.IsMine && Input.GetKeyDown(KeyCode.K))
        {
            _shootRight = isFacingLeft ; 
            _canShoot = true;
            _animator.SetTrigger("Throw");
            _photonView.RPC("HandleShootAnimation",RpcTarget.Others);
        }
    }

    public void ShootProjectile()
    {
        if (_canShoot)
        {
            GameObject projectile;
            if (!_shootRight)
            {
                projectile = PhotonNetwork.Instantiate(projectilePrefab.name, leftSpawnPosition.position,
                    Quaternion.identity);
            }
            else
            {
                projectile = PhotonNetwork.Instantiate(projectilePrefab.name, rightSpawnPosition.position,
                    Quaternion.identity);
                projectile.GetComponent<PhotonView>().RPC("ChangeDirection", RpcTarget.AllBuffered);
            }

            _canShoot = false;
        }
    }

    #endregion

    #region RPCCalls

    [PunRPC]
    public void HandleShootAnimation()
    {
        _animator.SetTrigger("Shoot");
    }
    

    #endregion
    
}
