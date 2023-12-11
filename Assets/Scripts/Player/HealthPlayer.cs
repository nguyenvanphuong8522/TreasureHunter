using System;
using System.Collections;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private int health;
    public AnimationPlayer animationPlayer;
    public MoveMentPlayer moveMentPlayer;
    public CamShake camShake;
    public int maxHealth = 100;

    void Start()
    {
        health = maxHealth;
    }
    public void TakeDame(int dame)
    {
        if (!animationPlayer.canBlink)
        {
            health += dame;
            EventManagerFuong<int>.TriggerEvent("UpdateHealBar", health);
            
            if (health > 0)
            {
                EmitVfx(dame);
                animationPlayer.ChangeAnimationState("hit");
            }
            else
            {
                GameManager.instance.gameState = GAMESTATE.END;
                animationPlayer.EffectDead();
                
                StartCoroutine(DelayShowPopUp());
            }
            moveMentPlayer.isTakeDamaging = true;
        }
    }

    IEnumerator DelayShowPopUp()
    {
        yield return new WaitForSeconds(1);
        UiPresent.Instance.ShowPopUp();
    }
    public void EmitVfx(int damage)
    {

        EffectPopUpDamage(damage);
        AudioManager.instance.PlaySfx("takedame");
        //Handheld.Vibrate();
        Invoke(nameof(DelayTakeDame), 0.3f);
        //Invoke(nameof(StopVibrate), 0.1f);
    }

    public void DelayTakeDame()
    {
        moveMentPlayer.isTakeDamaging = false;
        animationPlayer.SetCanBlink();
    }

    public void EffectPopUpDamage(int damage)
    {
        GameObject popUpDamage = ObjectPool.instance.Get(ObjectPool.instance.popUpDamage);
        popUpDamage.SetActive(true);
        popUpDamage.GetComponent<DameText>().SetText(damage);
        popUpDamage.transform.position = transform.position;
    }
    public void StopVibrate()
    {
        Handheld.StopActivityIndicator();
    }

    public void HealPowerUp(int _heal)
    {
        if(GameManager.instance.bottleHeal > 0)
        {
            health += _heal;
            GameManager.instance.bottleHeal--;
            UiPresent.Instance.UpdateUiPresent();
            EffectPopUpDamage(_heal);
            EventManagerFuong<int>.TriggerEvent("UpdateHealBar", health);
        }
        
    }
    public void SpeedPowerUp()
    {
        if(GameManager.instance.bottleSpeed > 0)
        {
            moveMentPlayer.speed *= 2;
            GameManager.instance.bottleSpeed--;
            UiPresent.Instance.UpdateUiPresent();
            Invoke(nameof(DelayDecreaseSpeed), 3);
        }
        
    }

    public void DelayDecreaseSpeed()
    {
        moveMentPlayer.speed /= 2;
    }
}
