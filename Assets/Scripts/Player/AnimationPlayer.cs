using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] public PlayerAttack playerAttack;
    [SerializeField] public MoveMentPlayer moveMentPlayer;
    [SerializeField] public SpriteRenderer spriteRenderer;
    private AudioManager audioManager;
    public string currentState;
    public bool falledAnim = true;
    public bool canBlink = false;
    private float timeDelay = 1;
    private void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
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
                if (moveMentPlayer.rb.velocity.y > 0)
                    ChangeAnimationState("jumpWithoutSword");
                else
                    ChangeAnimationState("fall02", 0f, 0, 0f);
            }
        }

        if(canBlink)
        {
            if(timeDelay > 0.24f)
            {
                PlayBlinkHit();
                timeDelay = 0;
            }
            timeDelay += Time.deltaTime;
        }
    }

    public void SetCantBlink()
    {
        canBlink = false;
    }
    public void SetCanBlink()
    {
        canBlink = true;
        Invoke(nameof(SetCantBlink), 1.2f);
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

    public void ChangeAnimationState(string newState, float transition, int layer, float timeOffse)
    {
        if (currentState == newState)
        {
            return;
        }
        animator.CrossFade(newState, transition, layer, timeOffse);
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

    public void PlayBlinkHit()
    {
        BlinkHit();
        Invoke(nameof(OffBlink), 0.12f);
    }

    public void BlinkHit()
    {
        Color colorHit = Color.white;
        colorHit.a = 0f;
        spriteRenderer.color = colorHit;
    }
    public void OffBlink()
    {
        Color colorHit = Color.white;
        colorHit.a = 1;
        spriteRenderer.color = colorHit;
    }

    public void EffectDead()
    {
        Vector2 directForce = new Vector2(-transform.localScale.x * 2, 3);
        moveMentPlayer.rb.velocity = Vector2.zero;
        moveMentPlayer.rb.AddForce(directForce * 3.5f, ForceMode2D.Impulse);
        gameObject.tag = "Untagged";
        ChangeAnimationState("deadHit");
        Vector3 pos = new Vector3(transform.position.x + transform.localScale.x, transform.position.y, transform.position.z);
        GameObject swordDead = Instantiate(ObjectPool.instance.swordDead, pos, Quaternion.identity);
        Vector3 localScale = new Vector3(transform.localScale.x * 6, 6, 6);
        swordDead.transform.localScale = localScale;
    }
}
