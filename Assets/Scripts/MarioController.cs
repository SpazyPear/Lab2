using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public int lives;
    public int coinCount;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        coinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coinCount++;
            collision.gameObject.SetActive(false);
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
}
