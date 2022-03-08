using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMovement : MonoBehaviour
{
    bool hitShell;
    

    // Start is called before the first frame update
    void Start()
    {
        hitShell = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hitShell)
        {
        transform.position += new Vector3(0.3f,0,0);
        }
        Destroy(this.gameObject, 5f);
        
    }
     private void OnCollisionEnter2D(Collision2D collision)
     { 
        hitShell = true;
         if (collision.gameObject.CompareTag("Enemy"))
         {
             Destroy(collision.gameObject);
         }
     }

    
}
