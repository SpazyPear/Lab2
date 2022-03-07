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
        Transform curTransform = transform.parent == null ? transform : transform.parent;
        curTransform.position += new Vector3(Input.GetAxis("Horizontal") * marioSpeed * Time.deltaTime, 0.0f, 0.0f);

        if ((curTransform.localScale.x > 0 && Input.GetAxis("Horizontal") < 0) || (curTransform.localScale.x < 0 && Input.GetAxis("Horizontal") > 0.1f))
            {
            curTransform.localScale = new Vector3(curTransform.localScale.x * -1.0f, curTransform.localScale.y, curTransform.localScale.z);
            }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            curTransform.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
        }

        

    }


}
