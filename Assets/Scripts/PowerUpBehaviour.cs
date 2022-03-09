using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Normal = 0, FireFlower = 1, Star = 2, Mushroom = 3}

public class PowerUpBehaviour : MonoBehaviour
{
    public State currentState = 0;
    public float starDuration = 5f;
    public SpriteRenderer marioRenderer;
    public Color[] colors;
    public float colorSpeed = 1f;
    public GameObject fireBall;
    bool FireFlowerActive = false;

    //BIGEFFECT WITH MUSHROOM
    [Header("BIG!")]
    public float bigDuration;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && FireFlowerActive)
        {
            GameObject fireBallObj = Instantiate(fireBall, new Vector3(marioRenderer.gameObject.transform.position.x + marioRenderer.gameObject.transform.localScale.x, marioRenderer.gameObject.transform.position.y), Quaternion.identity);
            fireBallObj.GetComponent<Bounce>().forward = transform.localScale.x;
            fireBallObj.GetComponent<Rigidbody2D>().AddForce(new Vector3(marioRenderer.gameObject.transform.localScale.x * 4f, 0), ForceMode2D.Impulse);
        }
    }

    public void changeState(State newState)
    {
        currentState = newState;
        switch (newState)
        {
            case State.FireFlower:
                FireFlowerActive = true;
                break;
            case State.Star:
                StartCoroutine(starEffect());
                break;
            case State.Mushroom:
                StartCoroutine(bigEffect());
                break;
        }
    }

    IEnumerator starEffect()
    {
        float timer = starDuration;
        int curColorIndex = 0;
        float curColorTimer = colorSpeed;

        while (timer > 0)
        {
              
            timer -= Time.deltaTime;
            curColorTimer -= Time.deltaTime;
            float progress = (colorSpeed - curColorTimer) / colorSpeed;
            marioRenderer.color = Color.Lerp(colors[curColorIndex], colors[curColorIndex + 1], progress);
            if (progress > 1)
            {
                curColorIndex = curColorIndex >= colors.Length - 2 ? 0 : curColorIndex + 1;
                curColorTimer = colorSpeed;
            }
            yield return null;
        }

        marioRenderer.color = Color.white;
        currentState = 0;
    }
    IEnumerator bigEffect()
    {
        float bigDuration = 2f;
        while (bigDuration > 0)
        {
            bigDuration -= Time.deltaTime;
            marioRenderer.size = new Vector2(2, 2);
        }
        yield return null;
    }
}
