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

    public GameObject shell;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        coinCount = 0;
        //shell = GameObject.FindWithTag("Shell");
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
            collision.gameObject.transform.localScale = new Vector3(1, 0.1f, 1);
            Destroy(collision.gameObject, 0.3f);
        }
        else if (collision.gameObject.CompareTag("KoopaTroopa") && velocity.y < 0)
        {
            Vector3 shellPosition = collision.gameObject.transform.localPosition;
            Destroy(collision.gameObject);
            Instantiate(shell, shellPosition, Quaternion.identity);
        }
    }

    IEnumerator canHurtTimer()
    {
        yield return new WaitForSeconds(1f);
        canHurt = true;
    }
}
