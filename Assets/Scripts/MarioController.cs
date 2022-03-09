using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarioController : MonoBehaviour
{
    public int lives;
    public int coinCount;
    public int score;

    bool canHurt = true;
    public Vector3 velocity;
    Transform curTransform;
    Vector3 prevPos;

    public Text coinCounter;
    public Text lifeCounter;
    public Text scoreCounter;


    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        updateLivesCounter();
        coinCount = 0;
        updateCoinCounter();
        score = 0;
        updateScoreCounter();
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
            score += 200;
            Destroy(collision.gameObject);
            if (coinCount == 100)
            {
                ExtraLife();
                coinCount = 0;
            }
            updateCoinCounter();
            updateScoreCounter();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PipeDown")
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
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
        updateLivesCounter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && velocity.y >= 0) {
            lives--;
            updateLivesCounter();
            canHurt = false;
        }
        else if (collision.gameObject.CompareTag("Enemy") && velocity.y < 0)
        {
            score += 100;
            Destroy(collision.gameObject);
        }
        updateScoreCounter();
    }

    IEnumerator canHurtTimer()
    {
        yield return new WaitForSeconds(1f);
        canHurt = true;
    }

    void updateCoinCounter()
    {
        coinCounter.text = coinCount.ToString();
    }

    void updateLivesCounter()
    {
        lifeCounter.text = lives.ToString();
    }

    void updateScoreCounter()
    {
        scoreCounter.text = score.ToString();
    }
}
