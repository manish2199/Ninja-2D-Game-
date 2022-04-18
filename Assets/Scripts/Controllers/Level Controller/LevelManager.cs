using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
   public static LevelManager instance;

   [SerializeField] string[] Levels;
 
   void Awake()
   {
       makeSingleton();
   }

   void makeSingleton()
   {
       if( instance != null)
       {
          Destroy(gameObject);
       }
       else
       {
           DontDestroyOnLoad(gameObject);
           instance = this;
       }
   }
   
    void Start()
   {
       if(GetLevelStatus(Levels[0]) == LevelState.Locked)
       {
           SetLevelStatus(Levels[0],LevelState.Unlocked);
       }
   }

   public void MarkCurrentLevelCompleted()
   {
       Scene scene = SceneManager.GetActiveScene();
       SetLevelStatus(scene.name , LevelState.Completed);

       int currentScene = Array.FindIndex(Levels,level => level == scene.name);
       int nextScene = currentScene + 1;
       SetLevelStatus(Levels[nextScene],LevelState.Unlocked);
   }
   

   public LevelState GetLevelStatus(string level)
   {
       LevelState levelStatus =(LevelState)PlayerPrefs.GetInt(level,0);
       return levelStatus;
   }

   public void SetLevelStatus(string level,LevelState status)
   {
        PlayerPrefs.SetInt(level,(int)status);
   }


}
