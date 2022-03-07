using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public float moveSpeed = 2f;
    Transform bottomWayPoint, topWayPoint;
    Vector3 localScale;
    bool movingUp = true;
    Rigidbody2D rb;

    //public AudioClip pickSound;


    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        bottomWayPoint = GameObject.Find("BottomWayPoint").GetComponent<Transform>();
        topWayPoint = GameObject.Find("TopWayPoint").GetComponent<Transform>();
    }

    void Update()
    {
        if (transform.position.y > topWayPoint.position.y)
            movingUp = false;
        if (transform.position.y < bottomWayPoint.position.y)
            movingUp = true;

        if (movingUp)
        {
            moveUp();
        }

        else
        {
            moveBottom();
        }
    }

    void moveUp()
    {
        movingUp = true;
        localScale.y = 1;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.y * moveSpeed, rb.velocity.y);
    }


    void moveBottom()
    {
        movingUp = false;
        localScale.y = -1;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.y * moveSpeed, rb.velocity.y);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.transform.tag == "Player" && HelicopterMovement.numOfSoldiers < 3)
    //    {
    //        HelicopterMovement.numOfSoldiers++;
    //        HelicopterMovement.totalSoldiers++;

    //        AudioSource.PlayClipAtPoint(pickSound, transform.position, 0.7f);
    //        Destroy(gameObject);
    //    }
    //}

}
