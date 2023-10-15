using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimation : MonoBehaviour
{
    public PlayerAttack playerAttack;
    public MoveMentPlayer moveMentPlayer;
    public AnimationPlayer animatinPlayer;

    public void StartAnimationFromPlayerAttack()
    {
        playerAttack.SrartAnim();
    }
    public void EndAnimationFromPlayerAttack()
    {
        playerAttack.EndAnim();
    }
    public void DelayGroundAnimation()
    {
        moveMentPlayer.DelayFall();
    }
    public void EmitRunParticle()
    {
        animatinPlayer.EmitRunParticle();
    }
}
