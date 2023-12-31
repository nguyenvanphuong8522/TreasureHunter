using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePowerUp : MonoBehaviour
{
    public int id;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            MoveMentPlayer.instance.SpawnEffectCollect(1, collision.gameObject.transform.position, 0.4f);
            if (id == 1)
            {
                GameManager.instance.bottleHeal++;
            }
            else if(id == 2)
            {
                GameManager.instance.bottleSpeed++;
            }
            else if(id == 3)
            {
                GameManager.instance.bottleAtk++;
            }
            AudioManager.instance.PlaySfx("coin");
            UiPresent.Instance.UpdateUiPresent();
            gameObject.SetActive(false);
        }
    }
}
