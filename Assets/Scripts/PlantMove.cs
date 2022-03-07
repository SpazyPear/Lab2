using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantMove : MonoBehaviour
{
    //private Vector3 posA;
    //private Vector3 posB;

    private float waitTime;
    public float startWaitTime;
    public float speed;

    //private Vector3 nextPos;

    //[SerializeField]
    //private Transform childTransform;

    //[SerializeField]
    //private Transform transformB;

    public Transform[] moveSpots;
    private int nextSpot;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        nextSpot = Random.Range(0, moveSpots.Length);
        //posA = childTransform.localPosition;
        //posB = transformB.localPosition;
        //nextPos = posB;
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

            //Move();
        }
    }
}

//private void Move()
//    {
//        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);

//        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
//        {
//            if (waitTime <= 0)
//            {
//                waitTime = startWaitTime;
//                ChangePosition();
//            }
//            else
//            {
//                waitTime -= Time.deltaTime;
//            }
//        }
//    }

//    private void ChangePosition()
//    {
//        nextPos = nextPos != posA ? posA : posB;
//    }

 
