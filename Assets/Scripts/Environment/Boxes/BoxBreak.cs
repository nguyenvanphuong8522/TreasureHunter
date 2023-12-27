using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreak : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Vector2 direct;
    [SerializeField]
    private float force;

    private void OnEnable()
    {
        Addforce();
        StartCoroutine(DelayReturnPool());
    }
    [Button]
    public void Addforce()
    {
        rb.AddForce(direct.normalized * force, ForceMode2D.Impulse);
    }
    public IEnumerator DelayReturnPool()
    {
        yield return new WaitForSeconds(4);
        ObjectPool.instance.Return(gameObject);
    }
}
