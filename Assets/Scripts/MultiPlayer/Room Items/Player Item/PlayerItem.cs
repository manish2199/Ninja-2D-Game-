using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Realtime;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
   #region SerializedFields

   [SerializeField] private TextMeshProUGUI playerNameTMPro;

   #endregion

   #region Public_Functions

   public void SetPlayerName(Player player)
   {
      playerNameTMPro.text = player.NickName;
   }

   #endregion
}
