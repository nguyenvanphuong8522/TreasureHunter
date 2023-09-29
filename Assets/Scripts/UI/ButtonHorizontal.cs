using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHorizontal : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private MoveMentPlayer moveMentPlayer;
    void Start()
    {
        button.onClick.AddListener(MoveLeft);
    }
    
    public void MoveLeft()
    {
        moveMentPlayer.horizontal = -1;
    }
}
