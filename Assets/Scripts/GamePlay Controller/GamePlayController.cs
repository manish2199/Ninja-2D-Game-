using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance ;

    [SerializeField] PlayerController playerScript;
    [SerializeField] GameObject playerGameObject;



    [SerializeField] GameObject[] checkPoints;

    void Awake()
    {
        makeInstance();
        // print(checkPoints.Length);

    }

    void makeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void hitLowerBoundary()
    {
        for(int i=checkPoints.Length - 1; i >= 0; i--)
        {
            // print(checkPoints[i].transform.localPosition.x);
            if(checkPoints[i].transform.position.x <= playerGameObject.transform.position.x )
            {
                Vector3 temp = checkPoints[i].transform.position;
                temp.x = checkPoints[i].transform.position.x;
                playerGameObject.transform.position = temp;
                break;
            }   
        }  
    }

    
    //==========================================================================================================================


    

    



}
