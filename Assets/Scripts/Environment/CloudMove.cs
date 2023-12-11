using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [Range(0, 1)]
    public float speed = 0.5f;
    public float offset;
    public Material material;
    private void Update()
    {
        offset += (Time.deltaTime * speed) / 10f;
        material.SetTextureOffset("_MainTex", new Vector2 (offset, 0));
    }
}
