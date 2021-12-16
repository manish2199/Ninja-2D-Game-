using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumObstacle : MonoBehaviour
{
    public float moveSpeed ;
    public float rightangle;
    public float leftangle;

    bool moveClockwise;

    Rigidbody2D rb;
    
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       moveClockwise = true;
    }


    void Update()
    {
        MovePendulum();
    }

    void ChangeDirection()
    {
        if ( transform.rotation.z > rightangle)
        {
            moveClockwise = false;
        }
        if ( transform.rotation.z < leftangle )
        {
            moveClockwise = true;
        }
    }


    void MovePendulum()
    {
        ChangeDirection();

        if (moveClockwise)
        {
           rb.angularVelocity = moveSpeed;
        }
        if ( !moveClockwise)
        {
            rb.angularVelocity = -1 * moveSpeed;
        }
    }
}
