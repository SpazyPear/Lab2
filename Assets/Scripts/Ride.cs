using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ride : MonoBehaviour
{
    GameObject player;
    bool shouldCollide = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player)
        {
            shouldCollide = false;
            StartCoroutine(collisionWait());
            player.transform.SetParent(null);
            player.gameObject.GetComponent<Rigidbody2D>().simulated = true;

            /*player.gameObject.GetComponent<Rigidbody2D>().simulated = true;
            player.gameObject.GetComponent<Collider2D>().enabled = true;*/

        }
    }

    IEnumerator collisionWait()
    {
        yield return new WaitForSeconds(1.5f);
        shouldCollide = true;
        Physics2D.IgnoreCollision(player.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), false);
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && shouldCollide)
        {
            collider.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            //collider.gameObject.GetComponent<Collider2D>().enabled = false;
            Physics2D.IgnoreCollision(collider.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
            collider.transform.SetParent(gameObject.transform);
            collider.transform.localPosition = new Vector3(0.26f, 0.53f);
            player = collider.gameObject;
        }
    }
}
