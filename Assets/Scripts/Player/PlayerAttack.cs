using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AnimationPlayer animationPlayer;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private MoveMentPlayer moveMentPlayer;
    public bool attacking = false;
    public bool isAttackPressed = true;
    public float isAttakDelay = 0.3f;
    public int combo = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
    }

    public void SrartAnim()
    {
        isAttackPressed = false;
        if (combo < 2)
        {
            combo++;
        }
    }

    public void EndAnim()
    {
        isAttackPressed = false;
        attacking = false;
        combo = 0;
    }
    public void Attack()
    {
            if (!isAttackPressed)
            {
                isAttackPressed = true;
                attacking = true;
                attackArea.SetActive(attacking);
                if (moveMentPlayer.IsGrounded())
                {
                    EmitEffectSword(combo);
                    AudioManager.instance.PlaySfx("attack0" + combo);
                    animationPlayer.ChangeAnimationState("attack0" + combo);
                }
                else
                {
                    EmitEffectSword(combo);
                    AudioManager.instance.PlaySfx("attack0" + combo);
                    animationPlayer.ChangeAnimationState("attack0" + combo);

                }
            }
    }
    public void EmitEffectSword(int index)
    {
        GameObject newParticle = ObjectPool.instance.Get(ObjectPool.instance.swordParticles[index]);
        newParticle.SetActive(true);
        newParticle.transform.localScale = new Vector3(5f, 5f, 1);
        Vector3 _localScale = newParticle.transform.localScale;
        newParticle.transform.localScale = new Vector3(_localScale.x * transform.localScale.x, _localScale.y, _localScale.z);
        newParticle.transform.position = transform.position + new Vector3(1.5f * transform.localScale.x, -0.2f, 0);
    }
}
