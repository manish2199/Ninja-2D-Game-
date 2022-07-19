using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class UIHandler : MonoBehaviour
{
   [SerializeField] private InputField createRoomNameIF;
   
   [SerializeField] private InputField joinRoomNameIF;

   public string CreateRoomNameTxt
   {
      get
      {
         return createRoomNameIF.text;
      }
   }
   
   public string JoinRoomNameTxt
   {
      get
      {
         return joinRoomNameIF.text;
      }
   }
}
