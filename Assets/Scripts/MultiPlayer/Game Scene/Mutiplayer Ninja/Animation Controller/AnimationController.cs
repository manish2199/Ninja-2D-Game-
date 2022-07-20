using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private float XAxisInput;

    private PhotonView _photonView;
    
    public void SetAnimatorVariables(Animator animator,PhotonView photonView)
    {
        _animator = animator;
        _photonView = photonView;
    }

    public void HandleAnimation(float xAxisInput)
    {
        XAxisInput = xAxisInput;
        _animator.SetFloat("Speed", Mathf.Abs(XAxisInput)); 
        _photonView.RPC("HandleMultiplayerAnimation",RpcTarget.Others);

    }
    
    [PunRPC]
    private void HandleMultiplayerAnimation()
    {
        _animator.SetFloat("Speed", Mathf.Abs(XAxisInput));        
    }
    
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         stream.SendNext(XAxisInput);
    //     }
    //     else if (stream.IsReading)
    //     {
    //         XAxisInput = (float)stream.ReceiveNext();
    //     }
    // }
}

