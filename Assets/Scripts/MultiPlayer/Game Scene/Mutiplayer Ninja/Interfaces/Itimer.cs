using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public interface ITimer
{ 
   void InitializeTimer(TextMeshProUGUI timerTextMeshProUGUI,PhotonView photonView);

   float CurrentTime { get; }

   string CurrentTimeText { get; }
   
   void TimerFunction();
   
   void StartTimer();

   void StopTimer();
}
