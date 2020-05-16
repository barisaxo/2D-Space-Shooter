using UnityEngine;

public class ZombieSub : MonoBehaviour
{
    GameObject player;
    Transform playerTF;
    Sprite zombieSprite;
    float speed;
    int color;
    public bool r, g, b, hit, bite;

    
    void Start()
    {
        zombieSprite = GameObject.Find("_GridManager").GetComponent<GridManager>().zombie;
        speed = Random.Range(1.1f, 1.8f);
        color = Random.Range(0, 7);
        player = GameObject.Find("Player");
        playerTF = player.transform;
        RiseAgain(); 
    }

    void OnCollisionEnter2D(Collision2D collision)//to bite the player
    {
        if (collision.collider.gameObject.GetComponent<PlayerSub>() != null)
            bite = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<PlayerSub>() != null)
            bite = false;
    }

    void RiseAgain()//creates a zombie to chase and eat player
    {
        gameObject.AddComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.AddComponent<SpriteRenderer>().sprite = zombieSprite;
        gameObject.transform.localScale = new Vector3(.5f, .5f, 1f);
        GetComponent<Rigidbody2D>().freezeRotation = true;

        if (color == 1) { r = true; } 
        if (color == 2) { g = true; }
        if (color == 3) { r = true; g = true; }
        if (color == 4) { b = true; }
        if (color == 5) { r = true; b = true; }
        if (color == 6) { g = true; b = true; }
        if (color == 0) { r = true; g = true; b = true; }

        Hit();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTF.position, 2 * speed * Time.deltaTime);

        if (bite == true)
        { player.GetComponent<PlayerSub>().TheyreEatingMeeeee(); }

        if (hit == true)
        {
            hit = false;
            Hit();
        }
    }


    private void Hit()//react to the bullet that hit you
    {
        if (g == true && b == true && r == true)
        { gameObject.GetComponent<SpriteRenderer>().color = new Color(.85f, .85f, .85f); return; }       

        if (r == true && g == true)
        { gameObject.GetComponent<SpriteRenderer>().color = new Color(.95f, .95f, .05f); return; }
        if (r == true && b == true)
        { gameObject.GetComponent<SpriteRenderer>().color = new Color(.85f, .15f, .85f); return; }
        if (g == true && b == true)
        { gameObject.GetComponent<SpriteRenderer>().color = new Color(.15f, .85f, .85f); return; }

        if (r == true)
        { gameObject.GetComponent<SpriteRenderer>().color = new Color(.85f, .15f, .15f); return; }
        if (g == true)
        { gameObject.GetComponent<SpriteRenderer>().color = new Color(.15f, .85f, .15f); return; }
        if (b == true)
        { gameObject.GetComponent<SpriteRenderer>().color = new Color(.15f, .15f, .85f); return; }

        if (r == false && g == false && b == false)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(.15f, .15f, .15f);
            gameObject.GetComponent<ZombieSub>().enabled = false;
            Destroy(gameObject, .75f);
        }
    }
}


