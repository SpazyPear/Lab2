using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text timerTextElement;
    public Text coinTextElement;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 999;
        coinTextElement.text = "x" + 0;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        DisplayTime();
    }

    void DisplayTime()
    {
        timerTextElement.text = "TIME\n" + (int)time;
    }

    public void UpdateCoins(int coins)
    {
        coinTextElement.text = "x" + coins;
    }
}
