using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    bool isColliding = false;
    Rigidbody2D rb;
    float timer;
    public float timerDuration = 2f;
    public float speed = 2f;
    CancellationTokenSource token;
    public Tilemap tileMap;
    bool canTurn = true;


    // Start is called before the first frame update
    async void Start()
    {
        tileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
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
            int direction = currentSpeed > 0 ? 1 : -1;
            TileBase tileBelow = tileMap.GetTile(tileMap.WorldToCell(transform.position + new Vector3(direction, -1)));
            TileBase tileSide = tileMap.GetTile(tileMap.WorldToCell(transform.position + new Vector3(direction, 0)));


            if (!isColliding && tileBelow != null && tileSide == null)
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
        if (collision.gameObject.CompareTag("Player"))
            isColliding = true;

        if (collision.gameObject.CompareTag("Fireball"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))

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
