using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChest : Box
{
    public override void SpawnEffect(int n, Vector3 pos)
    {
        for (int i = 0; i < n; i++)
        {
            EffectBreakBox(pos, i);
        }
    }
    public override void AnimationTakeDame()
    {
        PlayAnimation("hit");
        StartCoroutine(DelayAnimation(0.1f));
    }
    public override void EffectBreakBox(Vector3 pos, int index)
    {
        if(index > 9)
        {
            index = Random.Range(0, 10); 
        }
        GameObject break01 = ObjectPool.instance.Get(ObjectPool.instance.coins[index]);
        break01.transform.position = pos;
        break01.transform.localScale = Vector3.one * 5;
        break01.SetActive(true);
    }
    public override void Die()
    {
        Vector3 pos = transform.position;
        SpawnEffect(15, pos);
        PlayAnimation("die");
        box.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }


}
