using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    Rigidbody2D rb;
    const float bounceForce = 4f;
    public float forward = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(destroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tilemap"))
        {
            rb.velocity = new Vector3(4f * forward, bounceForce);
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("KoopaTroopa") ||  collision.gameObject.CompareTag("Plant"))
        {
            Destroy(collision.gameObject); 
        }
    }

    IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
