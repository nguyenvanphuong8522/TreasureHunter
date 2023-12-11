using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public MoveMentPlayer movementPlayer;
    public AnimationPlayer animationPlayer;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Ground"))
    //    {
    //        movementPlayer.grounded = true;
    //    }
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            movementPlayer.grounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            movementPlayer.falled = false;
            animationPlayer.falledAnim = false;
            movementPlayer.grounded = false;
        }
    }

}
