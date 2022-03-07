using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

        if ((transform.localScale.x > 0 && Input.GetAxis("Horizontal") < 0) || (transform.localScale.x < 0 && Input.GetAxis("Horizontal") > 0.1f))
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1.0f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
        }

        

    }


}
