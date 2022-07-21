using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class UIHandler : MonoBehaviour
{
   #region Serialized_Fields
   [SerializeField] private TMP_InputField createRoomNameIF;
   
   [SerializeField] private TMP_InputField joinRoomNameIF;
   #endregion
   
   #region Getters_And_Setters
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
   #endregion
}
