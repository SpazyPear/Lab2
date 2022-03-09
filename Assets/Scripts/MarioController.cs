using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

   public GameObject shell;

    bool liveSubtract;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        updateLivesCounter();
        coinCount = 0;
        updateCoinCounter();
        score = 0;
        updateScoreCounter();
        liveSubtract = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        PlayerPrefs.SetInt("Life Counter", lives);

        curTransform = transform.parent == null ? transform : transform.parent;
        velocity = (curTransform.position - prevPos) / Time.deltaTime;
        if(transform.localPosition.y<-5 && liveSubtract)
        {
            updateLivesCounter();
            canHurt = false;
            SceneManager.LoadScene("SampleScene");
            lives--;
            liveSubtract = false;
        }
          
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
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Plant") || collision.gameObject.CompareTag("KoopaTroopa")) && velocity.y >= 0) {
            
            updateLivesCounter();
            canHurt = false;
            SceneManager.LoadScene("SampleScene");
            lives--;
        }
        else if (collision.gameObject.CompareTag("Enemy") && velocity.y < 0)
        {
            score += 100;
            collision.gameObject.transform.localScale = new Vector3(1, 0.1f, 1);
            Destroy(collision.gameObject, 0.3f);
        }
        else if (collision.gameObject.CompareTag("KoopaTroopa") && velocity.y < 0)
        {
            Vector3 shellPosition = collision.gameObject.transform.localPosition;
            score += 200;
            Destroy(collision.gameObject);
            Instantiate(shell, shellPosition, Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("Tilemap") && velocity.y > 0)
        {
            score += 50;
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            score += 1000;
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
