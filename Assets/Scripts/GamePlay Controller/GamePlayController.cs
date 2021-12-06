using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance ;

   

    void Awake()
    {
        makeInstance();
    }

    void makeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    
    //==========================================================================================================================


    



}
