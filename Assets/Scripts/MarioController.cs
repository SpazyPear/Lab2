using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MarioController : MonoBehaviour
{

    public AudioClip deathSound;
    public AudioClip breakbrickSound;
    public AudioClip shellSound;
    public AudioClip goombaSound;
    public AudioClip coinSound;
    public AudioClip pipeSound;
    public AudioClip powerupAppearSound;
    public AudioClip powerupSound;


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

        //PlayerPrefs.SetInt("Life Counter", lives);

        curTransform = transform.parent == null ? transform : transform.parent;
        velocity = (curTransform.position - prevPos) / Time.deltaTime;
        if(transform.localPosition.y<-15 && liveSubtract)
        {
            updateLivesCounter();
            canHurt = false;
            //SceneManager.LoadScene("SampleScene");
            lives=0;
            liveSubtract = false;
        }

        if(lives==0)
        {
            SceneManager.LoadScene("SampleScene");
        }
           lifeCounter.text = lives.ToString();
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
            AudioSource.PlayClipAtPoint(coinSound, transform.position, 1f);
            coinCount++;
            score += 200;
            Destroy(collision.gameObject);
            if (coinCount == 100)
            {
                ExtraLife();
                coinCount = 0;
            }
        }

        if (collision.gameObject.tag == "PowerUp")
        {
            AudioSource.PlayClipAtPoint(powerupSound, transform.position, 1f);
            score += 1000;
        }

        if (collision.gameObject.tag == "Plant")
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, 1f);
            updateLivesCounter();
            canHurt = false;
            lives--;
        }

        updateCoinCounter();
        updateScoreCounter();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PipeDown")
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                AudioSource.PlayClipAtPoint(pipeSound, transform.position, 1f);
                curTransform.position = collision.gameObject.GetComponent<PipeScript>().destination;
            }
        }
        if (collision.gameObject.tag == "PipeSide")
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                AudioSource.PlayClipAtPoint(pipeSound, transform.position, 1f);
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
        if ((collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("KoopaTroopa")) && velocity.y >= 0)
        {

            AudioSource.PlayClipAtPoint(deathSound, transform.position, 1f);
            updateLivesCounter();
            canHurt = false;
            //SceneManager.LoadScene("SampleScene");
            lives--;
        }
        else if (collision.gameObject.CompareTag("Enemy") && velocity.y < 0)
        {
            AudioSource.PlayClipAtPoint(goombaSound, transform.position, 1f);
            score += 100;
            collision.gameObject.transform.localScale = new Vector3(1, 0.1f, 1);
            Destroy(collision.gameObject, 0.3f);
            curTransform.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.CompareTag("KoopaTroopa") && velocity.y < 0)
        {
            AudioSource.PlayClipAtPoint(goombaSound, transform.position, 1f);
            Vector3 shellPosition = collision.gameObject.transform.localPosition;
            score += 200;
            Destroy(collision.gameObject);
            Instantiate(shell, shellPosition, Quaternion.identity);
        }
        else if (collision.gameObject.CompareTag("Tilemap") && velocity.y > 0)
        {
            AudioSource.PlayClipAtPoint(breakbrickSound, transform.position, 1f);
            score += 50;
        }

        if (collision.gameObject.CompareTag("SLevelPipe"))
        {
            transform.position = new Vector3(123, 1.2f, 0);
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
