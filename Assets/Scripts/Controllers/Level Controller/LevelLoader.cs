using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LevelLoader : MonoBehaviour
{
    [SerializeField] Button button;

    [SerializeField] string level;
    
   void Start()
   {
       button.onClick.AddListener( ()=> levelLoad());
   }

   void levelLoad()
   {
     LevelState levelState = LevelManager.instance.GetLevelStatus(level);

     switch(levelState)
     {
         case LevelState.Completed:
         SceneManager.LoadScene(level);
         break;

         case LevelState.Locked:
         // Play levelLocked Sound
         break;

         case LevelState.Unlocked :
         SceneManager.LoadScene(level);
         break;
     }
   }






}


