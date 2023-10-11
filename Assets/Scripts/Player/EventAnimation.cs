using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimation : MonoBehaviour
{
    public PlayerAttack playerAttack;

    public void StartAnimationFromPlayerAttack()
    {
        playerAttack.SrartAnim();
    }
    public void EndAnimationFromPlayerAttack()
    {
        playerAttack.EndAnim();
    }
}
