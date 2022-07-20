using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class UIHandler : MonoBehaviour
{
   [SerializeField] private TMP_InputField createRoomNameIF;
   
   [SerializeField] private TMP_InputField joinRoomNameIF;

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
