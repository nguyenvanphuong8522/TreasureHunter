using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public GameObject camera;

    private void LateUpdate()
    {
        transform.position = new Vector3(camera.transform.position.x, transform.position.y, 1);
    }
}
