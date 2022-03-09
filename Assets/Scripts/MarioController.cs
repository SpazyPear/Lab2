using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public int lives;
    public int coinCount;
    bool canHurt = true;
    public Vector3 velocity;
    Transform curTransform;
    Vector3 prevPos;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        coinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        curTransform = transform.parent == null ? transform : transform.parent;
        velocity = (curTransform.position - prevPos) / Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (curTransform)
            prevPos = curTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coinCount++;
            Destroy(collision.gameObject);
            if (coinCount == 100)
            {
                ExtraLife();
                coinCount = 0;
            }
            Camera.main.GetComponent<TextController>().UpdateCoins(coinCount);
        }
        if (collision.gameObject.tag == "PipeDown")
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("A");
                curTransform.position = collision.gameObject.GetComponent<PipeScript>().destination;
            }
        }
        if (collision.gameObject.tag == "PipeSide")
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                curTransform.position = collision.gameObject.GetComponent<PipeScript>().destination;
            }
        }
    }

    void ExtraLife()
    {
        lives++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && velocity.y >= 0) {
            lives--;
            canHurt = false;
        }
        else if (collision.gameObject.CompareTag("Enemy") && velocity.y < 0)
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator canHurtTimer()
    {
        yield return new WaitForSeconds(1f);
        canHurt = true;
    }
}
