using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AnimationPlayer animationPlayer;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private MoveMentPlayer moveMentPlayer;
    [SerializeField] private CamShake camShake;

    public int atk;
    public bool attacking = false;
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
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            AttackRanged();
        }
    }


    public void DeLayAttackAir()
    {
        attacking = false;
        attackArea.SetActive(false);
    }
    public void DeLayAttackRanged()
    {
        attacking = false;
    }

    IEnumerator DelayResetCombo(float time)
    {
        yield return new WaitForSeconds(time);
        combo = 0;
    }

    public void Attack()
    {
        if (GameManager.instance.gameState == GAMESTATE.START)
        {
            if (!attacking)
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
                    //Invoke(nameof(DeLayAttackMelee), 0.3f);
                    StartCoroutine(DeLayAttackMelee(0.3f));
                }
                else if (!moveMentPlayer.grounded)
                {
                    AudioManager.instance.PlaySfx("attack02");
                    animationPlayer.ChangeAnimationState("attack02");
                    EmitEffectSword(2);
                    Invoke(nameof(DeLayAttackAir), 0.4f);
                }
            }
        }
    }
    IEnumerator DeLayAttackMelee(float time)
    {
        yield return new WaitForSeconds(time);
        DelayAttack();
    }
    public void DelayAttack()
    {
        if (combo < 2)
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
    public void AttackRanged()
    {
        if (GameManager.instance.gameState == GAMESTATE.START)
        {
            if (!attacking)
            {
                attacking = true;
                AudioManager.instance.PlaySfx("attackRanged");
                animationPlayer.ChangeAnimationState("attackThrow");
                Invoke(nameof(ThrowSword), 0.1f);
                Invoke(nameof(DeLayAttackRanged), 0.4f);
            }
        }
    }

    public void ThrowSword()
    {
        GameObject newParticle = ObjectPool.instance.Get(ObjectPool.instance.swordParticles[3]);
        newParticle.SetActive(true);
        newParticle.transform.position = transform.position + new Vector3(1.5f * transform.localScale.x, 0.2f, 0);
        newParticle.transform.localScale = new Vector3(transform.localScale.x * 5f, 5f, 1);
        newParticle.GetComponent<SwordThrow>().camSake = camShake;
    }
    public void EmitEffectSword(int index)
    {
        GameObject newParticle = ObjectPool.instance.Get(ObjectPool.instance.swordParticles[index]);
        newParticle.SetActive(true);
        newParticle.transform.position = transform.position + new Vector3(1.5f * transform.localScale.x, -0.2f, 0);
        newParticle.transform.SetParent(transform);
        newParticle.transform.localScale = new Vector3(5f, 5f, 1);
    }

    public void AtkPowerUp()
    {
        if(GameManager.instance.bottleAtk > 0)
        {
            atk *= 2;
            GameManager.instance.bottleAtk--;
            UiPresent.Instance.UpdateUiPresent();
            Invoke(nameof(DelayDecreaseAtk), 3);
        }
       
    }

    public void DelayDecreaseAtk()
    {
        atk /= 2;
    }
}
