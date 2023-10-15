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
    [SerializeField] private CamShake camShake;
    public bool attacking = false;
    public bool isAttackPressed = true;
    public float isAttakDelay = 0.3f;
    public int combo = 0;
    private IEnumerator delayResetCombo;
    private void Start()
    {
        delayResetCombo = DelayResetCombo(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
    }

    public void SrartAnim()
    {
        if(combo <2)
        {
            delayResetCombo = DelayResetCombo(0.5f);
            StartCoroutine(delayResetCombo);
            combo++;
        }
        else
        {
            combo = 0;
        }
        attacking = false;
        attackArea.SetActive(false);
    }
    public void SrartAnimAir()
    {
        attacking = false;
        attackArea.SetActive(false);
    }

    IEnumerator DelayResetCombo(float time)
    {
        yield return new WaitForSeconds(time);
        combo = 0;
    }

    public void EndAnim()
    {
        isAttackPressed = false;
        attacking = false;
        attackArea.SetActive(attacking);
        combo = 0;
    }
    public void Attack()
    {
        if(!attacking)
        {
            attacking = true;
            attackArea.SetActive(true);
            StopCoroutine(delayResetCombo);
            if (moveMentPlayer.grounded)
            {
                camShake.ShakeCam();
                AudioManager.instance.PlaySfx("attack0" + combo);
                animationPlayer.ChangeAnimationState("attack0" + combo);
                EmitEffectSword(combo);
                Invoke(nameof(SrartAnim), 0.3f);
            }
            else if (!moveMentPlayer.grounded)
            {
                AudioManager.instance.PlaySfx("attack01");
                animationPlayer.ChangeAnimationState("attack01");
                EmitEffectSword(1);
                Invoke(nameof(SrartAnimAir), 0.4f);
            }
        }
        
    }
    public void EmitEffectSword(int index)
    {
        GameObject newParticle = ObjectPool.instance.Get(ObjectPool.instance.swordParticles[index]);
        newParticle.SetActive(true);
        newParticle.transform.position = transform.position + new Vector3(1.5f * transform.localScale.x, -0.2f, 0);
        newParticle.transform.SetParent(transform);
        newParticle.transform.localScale = new Vector3(5f, 5f, 1);
    }
}
