using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] public PlayerAttack playerAttack;
    [SerializeField] public MoveMentPlayer moveMentPlayer;
    private AudioManager audioManager;
    public string currentState;
    public bool falledAnim = true;
    private void Start()
    {
        audioManager = AudioManager.instance;
    }
    private void Update()
    {
        if (!moveMentPlayer.isTakeDamaging && !playerAttack.attacking)
        {
            if (moveMentPlayer.grounded)
            {
                if (!falledAnim)
                {
                    if (!moveMentPlayer.falled)
                    {
                        audioManager.PlaySfx("fall");
                        EmitParticle("fallParticle", new Vector3(0, -0.18f, 0), new Vector3(5, 5, 1));
                        moveMentPlayer.falled = true;
                        ChangeAnimationState("groundS");
                        Invoke(nameof(DelayAnimationGroundS), 0.2f);
                    }
                }
                else
                {
                    if (moveMentPlayer.horizontal != 0)
                    {
                        ChangeAnimationState("run02");
                    }
                    else
                    {
                        ChangeAnimationState("idle02");
                    }
                }
            }
            else
            {
                ChangeAnimationState("jumps");
            }
        }
    }


    public void DelayAnimationGroundS()
    {
        falledAnim = true;
    }
    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }

    public GameObject EmitParticle(string nameParticle, Vector3 offsetPos, Vector3 localScale)
    {
        GameObject newParticle = null;
        switch (nameParticle)
        {
            case "fallParticle":
                newParticle = ObjectPool.instance.Get(ObjectPool.instance.fallParticle);
                break;
            case "jumpParticle":
                newParticle = ObjectPool.instance.Get(ObjectPool.instance.jumpParticle);
                break;
            case "moveParticle":
                newParticle = ObjectPool.instance.Get(ObjectPool.instance.moveParticle);
                break;
            default:
                break;
        }
        newParticle.SetActive(true);
        newParticle.transform.position = transform.position + offsetPos;
        newParticle.transform.localScale = localScale;
        if (newParticle != null)
        {
            return newParticle;
        }
        else
        {
            return null;
        }
    }

    public void EmitRunParticle()
    {
        audioManager.PlaySfx("footstep");
        GameObject newParticle = EmitParticle("moveParticle", new Vector3(0, -0.1f, 0), new Vector3(5f, 5f, 1));
        Vector3 _localScale = newParticle.transform.localScale;
        newParticle.transform.localScale = new Vector3(_localScale.x * transform.localScale.x, _localScale.y, _localScale.z);
    }
}
