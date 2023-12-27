using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxBarrel : Box
{
    public override void SpawnEffect(int n, Vector3 pos)
    {
        int index = Random.Range(0, 3);
        GameObject bottle = ObjectPool.instance.Get(ObjectPool.instance.bottles[index]);
        bottle.transform.position = pos;
        bottle.transform.localScale = Vector3.one * 6;
        bottle.SetActive(true);

        base.SpawnEffect(n, pos);
    }
    public override void EffectBreakBox(Vector3 pos, int index)
    {
        GameObject break01 = ObjectPool.instance.Get(ObjectPool.instance.breaksBoxBarrel[index]);
        break01.transform.position = pos;
        break01.transform.localScale = Vector3.one * 5;
        break01.SetActive(true);
    }
}
