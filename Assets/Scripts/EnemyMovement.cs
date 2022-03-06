using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    bool isColliding = false;
    Rigidbody2D rb;
    float timer;
    public float timerDuration = 2f;
    public float speed = 2f;
    CancellationTokenSource token;


    // Start is called before the first frame update
    async void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = timerDuration;
        token = new CancellationTokenSource();
        await waitToStart();
        turnTimer(token.Token);
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    async Task waitToStart()
    {
        await Task.Delay(UnityEngine.Random.Range(0, 1000));
    }

    async void turnTimer(CancellationToken token = default)
    {
        float currentSpeed = speed;
        while (true)
        {
            if (!isColliding)
            {
                transform.position += new Vector3(Time.deltaTime * currentSpeed, 0f);
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    timer = timerDuration;
                    currentSpeed = -currentSpeed;
                }
            }
            else
            {
                currentSpeed = -currentSpeed;
                transform.position += new Vector3(Time.deltaTime * currentSpeed, 0f);
                isColliding = false;
            }
            await Task.Yield();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            isColliding = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
            isColliding = false;
    }

    void OnApplicationQuit() 
    {
        #if UNITY_EDITOR
            var constructor = SynchronizationContext.Current.GetType().GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(int) }, null);
            var newContext = constructor.Invoke(new object[] { Thread.CurrentThread.ManagedThreadId });
            SynchronizationContext.SetSynchronizationContext(newContext as SynchronizationContext);
        #endif
    }
}
