using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DameText : MonoBehaviour
{
    public TextMeshPro dameText;
    private void OnEnable()
    {
        Invoke(nameof(DelayReturn), 0.5f);
    }
    public void DelayReturn()
    {
        ObjectPool.instance.Return(gameObject);
    }

    public void SetText(int value)
    {
        SetColor(value);
        dameText.SetText(value + "");
    }

    public void SetColor(int value)
    {
        if(value > 0)
            dameText.color = Color.green;
        else
            dameText.color = Color.red;
    }
}
