using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text timerTextElement;
    public float time;

    void Start()
    {
        UpdateTime();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        UpdateTime();
    }

    void UpdateTime()
    {
        timerTextElement.text = ((int)time).ToString();
    }
}
