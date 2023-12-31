using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFly : MonoBehaviour
{
    public Vector2 direct;
    public Rigidbody2D rb;
    public float force;
    public bool isCannon;
    private void OnEnable()
    {
        Shoot();
        StartCoroutine(DelayReturnPool());

    }
    public IEnumerator DelayReturnPool()
    {
        yield return new WaitForSeconds(5);
        ObjectPool.instance.Return(gameObject);
    }
    private void Shoot()
    {

        rb.AddForce(direct.normalized * force, ForceMode2D.Impulse);
    }
    public void SpawnEffectCollect(int index, Vector3 pos, float time)
    {
        GameObject effect = ObjectPool.instance.Get(ObjectPool.instance.collectEffects[index]);
        effect.transform.position = pos;
        effect.transform.localScale = Vector3.one;
        effect.SetActive(true);
        StartCoroutine(DelayReturn(effect, time));
    }
    IEnumerator DelayReturn(GameObject x, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.instance.Return(x);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            if (collision.gameObject.CompareTag("Player") && isCannon)
            {
                SpawnEffectCollect(2, collision.gameObject.transform.position, 0.3f);
                AudioManager.instance.PlaySfx("boom");
            }

            ObjectPool.instance.Return(gameObject);
        }
    }
}
