﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D rgb;

   
    [SerializeField]float speed;
    [SerializeField]float timer;

    float dir;
    

    public void shootProjectile(bool isNinjaFacingLeft)
    {
        if (isNinjaFacingLeft)
        {
            rgb.velocity = new Vector2(-speed , 0 );
            transform.eulerAngles = new Vector3 ( 0 ,0 ,90);
            dir = 90; 
        }
        if (!isNinjaFacingLeft)
        {
            rgb.velocity = new Vector2(speed , 0 );
            transform.eulerAngles = new Vector3 ( 0 ,0 , -90);
             dir = -90;
        }
    }

    void OnCollisionEnter2D( Collision2D target)
    {
        if (target.gameObject.CompareTag("ClimbingPlatform") || target.gameObject.CompareTag("Platform"))
        {
            rgb.constraints = RigidbodyConstraints2D.FreezeAll;
        } 
    }

   void OnEnable()
    {
        Destroy(gameObject,timer);
    }



}
