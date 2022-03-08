using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMove : MonoBehaviour
{

    private float waitTime;
    public float startWaitTime;
    public float speed;


    public Transform[] moveSpots;
    private int nextSpot;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        nextSpot = Random.Range(0, moveSpots.Length);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[nextSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[nextSpot].position) <= 0.1)
        {
            if (waitTime <= 0)
            {
                nextSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }
    }
}


