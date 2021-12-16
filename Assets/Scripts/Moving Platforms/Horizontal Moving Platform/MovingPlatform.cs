using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  MovingPlatform : MonoBehaviour
{
  
    public Transform pos1,pos2;
    public float speed;
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
        movePlatform();
    }


    void movePlatform()
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
           transform.position = Vector3.MoveTowards(transform.position,nextPos,speed *Time.deltaTime);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

}
