using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeObstacle : MonoBehaviour
{
  
    public Transform pos1,pos2;
    float speed;
    public float upwardSpeed;
    public float downwardSpeed;
    public Transform Startpos;
    Vector3 nextPos ; 
    public bool MovePlat = true ;


    // Start is called before the first frame update
    void Start()
    {
        nextPos = Startpos.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveSpikePlatform();
    }


    void moveSpikePlatform()
    {
        if(MovePlat)
        {
           if ( transform.position == pos1.position)
           {
            nextPos = pos2.position;
           }
          if ( transform.position == pos2.position)
           {
            nextPos = pos1.position;
           }
            
           setSpeed();
           transform.position = Vector3.MoveTowards(transform.position,nextPos,speed *Time.deltaTime);
        }
    }


    void setSpeed()
    {
        if(nextPos == pos2.position)
        {
            speed = downwardSpeed;
        }
        else
        {
            speed = upwardSpeed;
        }
    }

}
