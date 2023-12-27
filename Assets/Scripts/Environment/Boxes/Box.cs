using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private int maxHeal;
    [SerializeField] public int currentHeal;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] protected BoxCollider2D box;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackArea"))
        {
            TakeDame();
        }
    }

    public void TakeDame()
    {
        currentHeal--;
        if(currentHeal > 0)
        {
            AnimationTakeDame();
        }
        else
        {
            Die();
        }
    }
    public virtual void AnimationTakeDame()
    {
        PlayAnimation("hit");
        StartCoroutine(DelayAnimation(0.4f));
    }

    public void PlayAnimation(string name)
    {
        animator.Play(name);
    }
    public IEnumerator DelayAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        PlayAnimation("idle");
    }

    public virtual void Die()
    {
        Vector3 pos = transform.position;
        SpawnEffect(4, pos);
        Destroy(gameObject);
    }

    public virtual void SpawnEffect(int n, Vector3 pos)
    {
        for(int i = 0; i < n; i++)
        {
            EffectBreakBox(pos, i);
        }
    }

    public virtual void EffectBreakBox(Vector3 pos, int index)
    {
        GameObject break01 = ObjectPool.instance.Get(ObjectPool.instance.breaksBox[index]);
        break01.transform.position = pos;
        break01.transform.localScale = Vector3.one * 5;
        break01.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            AudioManager.instance.PlaySfx("swordstop");
            ObjectPool.instance.Return(collision.gameObject);
            TakeDame();
        }
    }
}
