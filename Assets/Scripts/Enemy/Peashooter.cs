using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : SeaShell
{
    public override void EffectBreakBox(Vector3 pos, int index)
    {
        GameObject break01 = ObjectPool.instance.Get(ObjectPool.instance.peaShooters[index]);
        break01.transform.position = pos;
        break01.transform.localScale = Vector3.one * 5;
        break01.SetActive(true);
    }
    public override IEnumerator SpawnBullet(Vector3 pos)
    {
        yield return new WaitForSeconds(0.4f);
        GameObject x = ObjectPool.instance.Get(ObjectPool.instance.bullets[2]);
        x.transform.position = pos;
        x.SetActive(true);
    }
    public override void Die()
    {
        Vector3 pos = transform.position;
        SpawnEffect(6, pos);
        Destroy(gameObject);
    }
}
