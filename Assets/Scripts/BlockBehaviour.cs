using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockBehaviour : MonoBehaviour
{
    public int marioSpeed;
    public Rigidbody2D rb;
    public float jumpForce = 3f;
    private Vector3 prevPos;
    private Vector3 velocity;
    [SerializeField]
    AudioClip brickBreakSound;
    [SerializeField]
    AudioSource audioSource;
    public Tilemap tileMap;
    public GameObject questionBlockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var position in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(position) && tileMap.GetTile(position).name.Equals("question_block"))
            {
                tileMap.SetTile(position, null);
                Vector3 gridPos = position + new Vector3(tileMap.cellSize.x / 2f, tileMap.cellSize.y / 2f);
                Instantiate(questionBlockPrefab, gridPos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        velocity = (transform.position - prevPos) / Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(velocity);

        if (collision.gameObject.CompareTag("Tilemap") && velocity.y > 0.5f)
        {

            if (audioSource && brickBreakSound)
            {
                audioSource.PlayOneShot(brickBreakSound);
            }
            foreach (ContactPoint2D hit in collision.contacts)
            {
                Vector2 hitPosition = new Vector2();
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                if (tileMap.GetTile(tileMap.WorldToCell(hitPosition)).name.Equals("brick_block"))
                    tileMap.SetTile(tileMap.WorldToCell(hitPosition), null);
            }

            if (audioSource && brickBreakSound)
            {
                audioSource.PlayOneShot(brickBreakSound);
            }
        }

        else if (collision.gameObject.CompareTag("QuestionBlock"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        }
    }

    private void FixedUpdate()
    {
        prevPos = transform.position;
    }


}
