using UnityEngine;

public class SpawnSub : MonoBehaviour
{
    GameObject player, zombie, map, gm;
    [SerializeField] float incubate;
    int health;



    void Start()
    {
        gm = GameObject.Find("_GridManager");
        zombie = new GameObject("Zombie");
        zombie.transform.SetParent(gameObject.transform);
        player = GameObject.Find("Player");
        health = 5;
        gameObject.AddComponent<BoxCollider2D>();
        map = GameObject.Find("MAP");
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            gm.GetComponent<GridManager>().NmeDestroyed();
            Destroy(gameObject, .2f);
        }
    }

    void Update()
    {
        var dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist < 25)
        {
            incubate += Time.deltaTime;
        }

        if (incubate >= 3f)
        {
            incubate -= 3f;  //Documentation says this is metronomically more accurate than: incubate = 0;
            SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        GameObject zomb = Instantiate(zombie, transform.position, Quaternion.identity);
        zomb.AddComponent<ZombieSub>();
        zomb.transform.SetParent(map.transform);
    }
}


// -IDEA-: shields - more difficult: shields in color order