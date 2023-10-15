using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputMobile : MonoBehaviour
{
    [SerializeField] private MoveMentPlayer movementPlayer;
    public bool isMoving = false;
    public bool jumbutton;

    public void MoveLeft()
    {
        movementPlayer.horizontal = -1;
        isMoving = true;
    }
    public void Idle()
    {
        movementPlayer.horizontal = 0;
        isMoving = false;
        jumbutton = false;
    }
    public void MoveRight()
    {
        movementPlayer.horizontal = 1;
        isMoving = true;
    }

    public void Jump()
    {
        jumbutton = true;
    }

    public void JumpExit()
    {
        jumbutton = false;
    }
}
