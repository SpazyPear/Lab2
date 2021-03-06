using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarioManager : MonoBehaviour
{
    public AudioClip jumpSound;

    public int marioSpeed;
    bool isGrounded = true;
    public MarioController marioController;
    bool canBeGrounded = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    async void Update()
    {
        if(gameObject.GetComponent<BlockBehaviour>().GameEnd != true)
        {
            Transform curTransform = transform.parent == null ? transform : transform.parent;
            curTransform.position += new Vector3(Input.GetAxis("Horizontal") * marioSpeed * Time.deltaTime, 0.0f, 0.0f);

            if ((curTransform.localScale.x > 0 && Input.GetAxis("Horizontal") < 0) || (curTransform.localScale.x < 0 && Input.GetAxis("Horizontal") > 0.1f))
            {
                curTransform.localScale = new Vector3(curTransform.localScale.x * -1.0f, curTransform.localScale.y, curTransform.localScale.z);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                AudioSource.PlayClipAtPoint(jumpSound, transform.position, 1f);
                isGrounded = false;
                canBeGrounded = false;
                curTransform.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 9, ForceMode2D.Impulse);
                await isGroundedTimer();
            }
        }
        else
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(saveprincess());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
         if (collision.gameObject.CompareTag("Tilemap") && canBeGrounded)
         {
            isGrounded = true;
         }
    }

    async Task isGroundedTimer()
    {
        await Task.Delay(200);
        canBeGrounded = true; 
    }
    IEnumerator saveprincess()
    {
        float runtoPrincess = 5;
        float speed = 1f;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 0, transform.position.z), speed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x+5 , 0, transform.position.z), speed * Time.deltaTime);
        Destroy(gameObject);
    }
}
