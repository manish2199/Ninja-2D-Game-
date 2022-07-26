using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public interface IJUMP
{
    bool CanJump { get; set; }
    void SetJumpVariables(Animator animator,float jumpForce);
    void HandleJump();
}
