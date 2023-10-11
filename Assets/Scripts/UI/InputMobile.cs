using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputMobile : MonoBehaviour
{
    [SerializeField] private MoveMentPlayer movementPlayer;
    public bool isMoving = false;
    public int isJumping = 0;

    public void MoveLeft()
    {
        movementPlayer.horizontal = -1;
        isMoving = true;
    }
    public void Idle()
    {
        movementPlayer.horizontal = 0;
        isMoving = false;
        isJumping = 0;
    }
    public void MoveRight()
    {
        movementPlayer.horizontal = 1;
        isMoving = true;
    }
    public void JumpUp()
    {
        isJumping = -1;
    }
    public void JumpDown()
    {
        isJumping = 1;
    }
}
