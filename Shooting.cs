using UnityEngine;

public class Shooting : MonoBehaviour
{
    GameObject map;
    GridManager gm;
    Transform cannon;
    Sprite bulletSprite;
    Color bulletColor;
    float firingSpeed;
    bool r, b, g;

    void Start()
    {
        gm = GameObject.Find("_GridManager").GetComponent<GridManager>();
        map = GameObject.Find("MAP");
        cannon = GameObject.Find("Cannon").transform;
        bulletSprite = GameObject.Find("_GridManager").GetComponent<GridManager>().bullet;
    }


    void Update() //Shooting with numpad or jkl
    {
        if ( Input.GetKey("[0]") || Input.GetKey("[1]") || Input.GetKey("[2]")
                 || Input.GetKey("[3]") || Input.GetKey("[4]") || Input.GetKey("[5]")
                         || Input.GetKey("[6]") || Input.GetKey("k") || Input.GetKey("j")
                                || Input.GetKey("l") || Input.GetKey("space") || Input.GetKey("u")
                                        || Input.GetKey("i") || Input.GetKey("o") )
        { firingSpeed += Time.deltaTime; }


        if (firingSpeed >= 0)
        {
            if (Input.GetKey("[2]") || Input.GetKey("k"))//Green
            {
                firingSpeed -= .35f;
                bulletColor = new Color(.15f, .85f, .15f);
                r = false; g = true; b = false;
                Shoot();//Green
                return;
            }

            if (Input.GetKey("[4]") || Input.GetKey("l"))//blue
            {

                firingSpeed -= .35f;
                bulletColor = new Color(.15f, .15f, .85f);
                r = false; g = false; b = true;
                Shoot();
                return;
            }

            if (Input.GetKey("[1]") || Input.GetKey("j"))//red
            {
                firingSpeed -= .35f;
                bulletColor = new Color(.85f, .15f, .15f);
                r = true; g = false; b = false;
                Shoot();
                return;
            }

            if (Input.GetKey("[3]") || Input.GetKey("u"))//Yellow
            {
                firingSpeed -= .5f;
                bulletColor = new Color(.85f, .85f, .15f);
                g = true; r = true; b = false;
                Shoot();
                return;
            }

            if (Input.GetKey("[5]") || Input.GetKey("i"))//Magenta
            {
                firingSpeed -= .5f;
                bulletColor = new Color(.85f, .15f, .85f);
                r = true; b = true; g = false;
                Shoot();
                return;
            }

            if (Input.GetKey("[6]") || Input.GetKey("o"))//Cyan
            {
                firingSpeed -= .5f;
                bulletColor = new Color(.15f, .85f, .85f);
                g = true; b = true; r = false;
                Shoot();
                return;
            }

            if (Input.GetKey("[0]") || Input.GetKey("space")) // White
            {
                firingSpeed -= 1f;
                bulletColor = new Color(.85f, .85f, .85f);
                r = true; b = true; g = true;
                Shoot();
                return;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = new GameObject("Bullet");
        bullet.transform.SetParent(map.transform);
        bullet.transform.position = cannon.transform.position;
        bullet.transform.rotation = cannon.transform.rotation;
        bullet.transform.localScale = new Vector3(.5f, .5f, 1f);
        bullet.AddComponent<BoxCollider2D>();
        bullet.AddComponent<SpriteRenderer>().sprite = bulletSprite;
        bullet.GetComponent<SpriteRenderer>().color = bulletColor;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        //bullet.GetComponent<Rigidbody2D>().mass = .1f;
        bullet.GetComponent<Rigidbody2D>().useAutoMass = true;
        //bullet.GetComponent<Rigidbody2D>().angularDrag = .001f;
        bullet.GetComponent<Rigidbody2D>().drag = .0f;
        bullet.AddComponent<BulletSub>().r = r;
        bullet.GetComponent<BulletSub>().g = g;
        bullet.GetComponent<BulletSub>().b = b;
        gm.audioSource.PlayOneShot(gm.pew);
    }
}