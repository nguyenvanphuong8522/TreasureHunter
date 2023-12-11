using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOneWay : MonoBehaviour
{
    public BoxCollider2D box;
    public Transform pointCheck;
    private void Update()
    {
        if(MoveMentPlayer.instance.transform.position.y > pointCheck.position.y)
        {
            box.enabled = true;
        }    
        else
        {
            box.enabled = false;
        }
    }
}
