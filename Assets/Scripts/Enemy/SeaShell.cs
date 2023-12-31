using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaShell : MonoBehaviour
{
    [SerializeField] private int maxHeal;
    [SerializeField] public int currentHeal;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] protected BoxCollider2D box;
    [SerializeField] protected float rateAttack;
    [SerializeField] private Transform pointShoot;
    private void Start()
    {
        StartCoroutine(Delay());
    }
    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(RateAttack());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackArea"))
        {
            int atk = collision.gameObject.GetComponentInParent<PlayerAttack>().atk;
            TakeDame(atk);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            AudioManager.instance.PlaySfx("swordstop");
            ObjectPool.instance.Return(collision.gameObject);
            TakeDame(10);
        }
    }

    public void TakeDame(int atk)
    {
        currentHeal -= atk;
        if (currentHeal > 0)
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
        for (int i = 0; i < n; i++)
        {
            EffectBreakBox(pos, i);
        }
    }
    public virtual void EffectBreakBox(Vector3 pos, int index)
    {
        GameObject break01 = ObjectPool.instance.Get(ObjectPool.instance.seaShell[index]);
        break01.transform.position = pos;
        break01.transform.localScale = Vector3.one * 5;
        break01.SetActive(true);
    }
    public void Attack()
    {
        PlayAnimation("attack");
        StartCoroutine(DelayAnimation(0.4f));
        if (Vector2.Distance(MoveMentPlayer.instance.transform.position, transform.position) < 20)
        {
            AudioManager.instance.PlaySfx("shoot");
            StartCoroutine(SpawnBullet(pointShoot.position));
        }
    }
    public virtual IEnumerator SpawnBullet(Vector3 pos)
    {
        yield return new WaitForSeconds(0.4f);
        GameObject x = ObjectPool.instance.Get(ObjectPool.instance.bullets[0]);
        x.transform.position = pos;
        x.SetActive(true);
    }

    public IEnumerator RateAttack()
    {
        while(true)
        {
            Attack();
            yield return new WaitForSeconds(rateAttack);
        }
    }

}
