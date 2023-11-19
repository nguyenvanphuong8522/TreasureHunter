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
        playerAttack.DeLayAttackMelee();
    }
    public void EmitRunParticle()
    {
        animatinPlayer.EmitRunParticle();
    }
}
