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
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        playerAttack.DelayAttack();
    }

    public void EmitRunParticle()
    {
        animatinPlayer.EmitRunParticle();
    }
}
