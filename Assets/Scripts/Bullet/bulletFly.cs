using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFly : MonoBehaviour
{
    public Vector2 direct;
    public Rigidbody2D rb;
    public float force;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            ObjectPool.instance.Return(gameObject);
        }
    }
}
