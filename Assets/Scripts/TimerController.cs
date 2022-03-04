using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text textElement;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 999;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        displayTime();
    }

    void displayTime()
    {
        textElement.text = "TIME\n" + (int)time;
    }
}
