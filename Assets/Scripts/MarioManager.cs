using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioManager : MonoBehaviour
{
    public int marioSpeed;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal") * marioSpeed * Time.deltaTime, 0.0f, 0.0f);

        if ((transform.localScale.x > 0 && Input.GetAxis("Horizontal") < 0) || (transform.localScale.x < 0 && Input.GetAxis("Horizontal") > 0))
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1.0f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
}
