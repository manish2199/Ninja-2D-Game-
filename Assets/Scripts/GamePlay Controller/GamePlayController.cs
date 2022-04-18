using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance ;

    [SerializeField] PlayerController playerScript;
    [SerializeField] NinjaStat playerStats;
    [SerializeField] GameObject playerGameObject;
    [SerializeField] Animator ninjaAnim;
    
    [SerializeField] GameObject[] checkPoints;

    void Awake()
    {
        makeInstance();
    }

    void Update()
    {
        // if(playerStats.Lives <=0)
        // {
        //   StartCoroutine(ninjaDead());
        // }
    }

    void makeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void respawnToNearestCheckPoint()
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



   IEnumerator ninjaDead()
   {
       ninjaAnim.SetTrigger("Dead"); 

       yield return new WaitForSeconds(1f);

       playerGameObject.SetActive(false);
   }
    

    



}
