using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCollistion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
       
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
            Debug.Log("check collision");
    }
}
