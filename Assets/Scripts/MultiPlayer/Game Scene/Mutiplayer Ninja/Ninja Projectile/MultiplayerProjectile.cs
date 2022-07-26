using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MultiplayerProjectile : MonoBehaviourPunCallbacks
{
    #region SerializedFields

    [SerializeField] private float speed;
    [SerializeField] private float timer;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private PhotonView photonView;

    #endregion

    #region Private_Fields

    private bool shootRight = false;

    private IEnumerator DestroyCoroutine;

    #endregion
    
    #region PublicFields

    public bool ShootRight
    {
        get { return shootRight;   }
        set { shootRight = value;  }
    }
    
    #endregion

    #region Getters_And_Setters

    public PhotonView ProjectilePhotonView => photonView;

    #endregion

    #region Unity_Funtions

    private void Awake()
    {
        DestroyCoroutine = DestroyProjectileRoutine();
    }

    private void Start()
    {
        StartCoroutine(DestroyCoroutine);
    }

    private void FixedUpdate()
    {
        ShootProjectile();
    }
    
    void OnCollisionEnter2D( Collision2D target)
    {
       if (target.gameObject.CompareTag("ClimbingPlatform") || target.gameObject.CompareTag("Platform"))
       {
          rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
          photonView.RPC("FreezePosition",RpcTarget.Others);
       } 
    }


    #endregion

    #region Public_Functions

    public IEnumerator DestroyProjectileRoutine()
    {
        yield return new WaitForSeconds(timer);
        photonView.RPC("DestroyProjectile",RpcTarget.AllBuffered);
    }

    #endregion
    
    #region Private_Funtions
    
    private void ShootProjectile() 
    {
        if (!shootRight)
        {
            rigidbody2D.velocity = new Vector2(-speed , 0 );
            transform.eulerAngles = new Vector3 ( 0 ,0 ,90);
            // = 90; 
        }
        if (shootRight)
        {
            rigidbody2D.velocity = new Vector2(speed , 0 );
            transform.eulerAngles = new Vector3 ( 0 ,0 , -90);
            // dir = -90;
        }
    }
    #endregion  
    
    #region RPC_Calls

    [PunRPC]
    public void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
    
    [PunRPC]
    public void ChangeDirection()
    {
        shootRight = true;
    }

    [PunRPC]
    public void FreezePosition()
    {
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    #endregion
    
}

     


