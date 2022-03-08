using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private Vector3 posA;
    private Vector3 posB;

    private float waitTime;
    public float startWaitTime;
    public float speed;

    private Vector3 nextPos;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);

        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        {
                ChangePosition();

        }
    }

    private void ChangePosition()
    {
        nextPos = nextPos != posA ? posA : posB;
    }


    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextPos) <= 0.1)
        {
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

            Move();
        }
    }
}




