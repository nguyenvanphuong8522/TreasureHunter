using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleDestroy : MonoBehaviour
{
    [SerializeField] private float durationDestroy = 0.5f;
    private void OnEnable()
    {
        Invoke(nameof(DeActive), durationDestroy);
    }
    public void DeActive()
    {
        ObjectPool.instance.Return(gameObject);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
