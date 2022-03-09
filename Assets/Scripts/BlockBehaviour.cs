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
    public Vector3 velocity;
    [SerializeField]
    AudioClip brickBreakSound;
    [SerializeField]
    AudioSource audioSource;
    public Tilemap tileMap;
    public GameObject questionBlockPrefab;
    public GameObject[] powerUps;
    public float powerUpPopUpSpeed = 1.5f;
    public PowerUpBehaviour powerUpManager;
    Transform curTransform;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var position in tileMap.cellBounds.allPositionsWithin)
        {
            if (tileMap.HasTile(position) && tileMap.GetTile(position).name.Equals("question_block"))
            {
                tileMap.SetTile(position, null);
                Vector3 gridPos = tileMap.CellToWorld(position) + new Vector3(tileMap.cellSize.x / 2f, tileMap.cellSize.y / 2f);
                Instantiate(questionBlockPrefab, gridPos, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        curTransform = transform.parent == null ? transform : transform.parent;
        velocity = (curTransform.position - prevPos) / Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (curTransform)
            prevPos = curTransform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

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

        else if (collision.gameObject.CompareTag("QuestionBlock") && velocity.y > 0.5f)
        {
            collision.gameObject.tag = "Untagged";
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hit");
            GameObject powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Length)], collision.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(powerUpPopUp(powerUp));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            switch (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name)
            {
                case "fire_flower":
                    powerUpManager.changeState(State.FireFlower);
                    break;
                case "starman":
                    powerUpManager.changeState(State.Star);
                    break;
                case "coin":
                    break;
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator powerUpPopUp(GameObject powerUp)
    {
        Vector3 startPos = powerUp.transform.position;
        while (powerUp.transform.position.y < tileMap.cellSize.y + startPos.y)
        {
            powerUp.transform.position += new Vector3(0f, powerUpPopUpSpeed * Time.deltaTime);
            yield return null;
        }
    }

}
