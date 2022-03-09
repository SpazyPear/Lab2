using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class BlockBehaviour : MonoBehaviour
{
    public int marioSpeed;
    public Rigidbody2D rb;
    public float jumpForce = 3f;
    private Vector3 prevPos;
    public Vector3 velocity;
    public Tilemap tileMap;
    public GameObject questionBlockPrefab;
    public GameObject[] powerUps;
    public float powerUpPopUpSpeed = 1.5f;
    public PowerUpBehaviour powerUpManager;
    Transform curTransform;

    //Game End 
    private Text text;
    public bool GameEnd;
    [SerializeField]
    private TextMeshProUGUI Again;
    [SerializeField]
    private TextMeshProUGUI Bye;

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
    //
    private void Awake()
    {
        //get font
        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        //creating canvas
        GameObject Canvas = new GameObject();
        Canvas.name = "Canvas";
        Canvas.AddComponent<Canvas>();
        Canvas.AddComponent<CanvasScaler>();
        Canvas.AddComponent<GraphicRaycaster>();
        Canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        //creating a text box
        GameObject textbox = new GameObject();
        textbox.transform.parent = Canvas.transform;
        textbox.AddComponent<Text>();
        text = textbox.GetComponent<Text>();
        text.font = arial;
        text.text = "";
        text.fontSize = 40;
        text.alignment = TextAnchor.MiddleCenter;
        RectTransform rect = textbox.GetComponent<RectTransform>();
        rect.localPosition = new Vector3(0, 0, 0);
        rect.sizeDelta = new Vector2(500, 200);
        /*//creating play again and quit button
        GameObject PlayAgainButton = new GameObject();
        PlayAgainButton.transform.parent = Canvas.transform;
        PlayAgainButton.AddComponent<RectTransform>();
        PlayAgainButton.AddComponent<Button>();
        PlayAgainButton.transform.position = new Vector3(200, 150, 0);
        PlayAgainButton.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        PlayAgainButton.GetComponent<Button>().onClick.AddListener(again);

        GameObject QuitButton = new GameObject();
        QuitButton.transform.parent = Canvas.transform;
        QuitButton.AddComponent<RectTransform>();
        QuitButton.AddComponent<Button>();
        QuitButton.transform.position = new Vector3(800, 150, 0);
        QuitButton.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        QuitButton.GetComponent<Button>().onClick.AddListener(bye);*/
    }
    //Play again and quit button method
    public void again()
    {
        SceneManager.LoadScene(0);
    }
    public void bye()
    {
        Application.Quit();
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

            foreach (ContactPoint2D hit in collision.contacts)
            {
                Vector2 hitPosition = new Vector2();
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                //hitPosition += new Vector2(1, 0);
                Debug.Log(tileMap.WorldToCell(hitPosition));
                TileBase tile = tileMap.GetTile(tileMap.WorldToCell(hitPosition));
                if (tile && tile.name == "brick_block")
                    tileMap.SetTile(tileMap.WorldToCell(hitPosition), null);
                
            }
        }

        else if (collision.gameObject.CompareTag("QuestionBlock") && velocity.y > 0.5f)
        {
            collision.gameObject.tag = "Untagged";
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hit");
            GameObject powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Length)], collision.gameObject.transform.position, Quaternion.identity);
            StartCoroutine(powerUpPopUp(powerUp));
        }
        //end game col with flag
        if (collision.gameObject.tag == "Flag")
        {
            //Debug.Log("hit the flag");
            GameEnd = true;
            
            text.text = "You Win! Princess is inside!";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            switch (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name)
            {
                case "fire_flower":
                    if (GameObject.Find("mario").GetComponent<Transform>().localScale.y < 1.5f)
                    {
                        powerUpManager.changeState(State.FireFlower);
                    }
                    else
                    {
                        Debug.Log("Not normal size. Cant shoot");
                    }
                    break;
                case "starman":
                    powerUpManager.changeState(State.Star);
            break;
                case "coin":
                    break;
                case "magic_mushroom":
                    powerUpManager.changeState(State.Mushroom);
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
