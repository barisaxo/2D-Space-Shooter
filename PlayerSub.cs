using UnityEngine;

public class PlayerSub : MonoBehaviour
{
    public int health, collisions;
    float  rc, gc, bc, shootColor;
    bool rs, gs, bs, warn;
    public float r, g, b;
    SpriteRenderer s;
    GameObject gm;

    void Start()
    {
        gm = GameObject.Find("_GridManager");
        health = 500;
        SetInitialColors();
        s = GetComponent<SpriteRenderer>();
    }


    public void TheyreEatingMeeeee()
    {
        health--;
        if (health <= 0)
        {
            gm.GetComponent<GridManager>().audioSource.volume = .6f;
            gm.GetComponent<GridManager>().audioSource.PlayOneShot(gm.GetComponent<GridManager>().died);
            
            GetComponent<MovementSub>().enabled = false;
            GameObject canvas = GameObject.Find("Canvas");
            GameObject Camera = GameObject.Find("Camera");
            Camera.transform.parent = canvas.gameObject.transform;
            Invoke("GameOver",2f);
        }
    }


    void SetInitialColors()
    {
        r = Random.Range(.20f, .80f);
        g = Random.Range(.20f, .80f);
        b = Random.Range(.20f, .80f);
        
        int rr = Random.Range(0, 2);
        if (rr == 1) { rs = true; }
        int gr = Random.Range(0, 2);
        if (gr == 1) { gs = true; }
        int br = Random.Range(0, 2);
        if (br == 1) { bs = true; }

        rc = Random.Range(.003f,.008f);
        gc = Random.Range(.003f,.008f);
        bc = Random.Range(.003f,.008f);
    }


    private void Update()
    {
        if( Input.GetKey("[0]") || Input.GetKey("space"))
        { int c = 0; Charge(c); return; }

        if (Input.GetKey("[1]") || Input.GetKey("j"))
        { int c = 1; Charge(c); return; }

        if (Input.GetKey("[2]") || Input.GetKey("k"))
        { int c = 2; Charge(c); return; }

        if (Input.GetKey("[3]") || Input.GetKey("u"))
        { int c = 3; Charge(c); return; }

        if (Input.GetKey("[4]") || Input.GetKey("l"))
        { int c = 4; Charge(c); return; }

        if (Input.GetKey("[5]") || Input.GetKey("i"))
        { int c = 5; Charge(c); return; }

        if (Input.GetKey("[6]") || Input.GetKey("o"))
        { int c = 6; Charge(c); return; }

        else { SetColors(); }
    }


    //Changes ship colors to charge up and match the bullet you are about to shoot.
    void Charge(int c)
    {
        if (c == 0)
        {
            shootColor += Time.deltaTime;
            if (shootColor >=1f)
            { shootColor -= 1f; }
            s.color = new Color(shootColor, shootColor, shootColor);
        }
        if (c == 1)
        {
            shootColor += Time.deltaTime;
            if (shootColor >= .35f)
            { shootColor -= .35f; }
            s.color = new Color(shootColor/.35f, .05f, .05f);
        }
        if (c == 2)
        {
            shootColor += Time.deltaTime;
            if (shootColor >= .35f)
            { shootColor -= .35f; }
            s.color = new Color(.05f, shootColor/.35f, .05f);
        }
        if (c == 3)
        {
            shootColor += Time.deltaTime;
            if (shootColor >= .5f)
            { shootColor -= .5f; }
            s.color = new Color(shootColor/.5f, shootColor/.5f, .05f);
        }
        if (c == 4)
        {
            shootColor += Time.deltaTime;
            if (shootColor >= .35f)
            { shootColor -= .35f; }
            s.color = new Color(.05f, .05f, shootColor/.35f);
        }
        if (c == 5)
        {
            shootColor += Time.deltaTime;
            if (shootColor >= .5f)
            { shootColor -= .5f; }
            s.color = new Color(shootColor/.5f, .05f, shootColor/.5f);
        }
        if (c == 6)
        {
            shootColor += Time.deltaTime;
            if (shootColor >= .5f)
            { shootColor -= .5f; }
            s.color = new Color(.05f, shootColor/.5f, shootColor/.5f);
        }
        
    }


    void SetColors()//phase through random colors, looks cool
    {
        s.color = new Color(r, g, b);
        if (rs == true) { r += rc * Time.deltaTime *25; }
        if (rs == false) { r -= rc * Time.deltaTime *25; }
        if (r >= .85f && rs == true) { rs = false; }
        if (r <= .05f && rs == false) { rs = true; }


        if (gs == true) { g += gc * Time.deltaTime *25; }
        if (gs == false) { g -= gc * Time.deltaTime*25; }
        if (g >= .85f && gs == true) { gs = false; }
        if (g <= .05f && gs == false) { gs = true; }

        if (bs == true) { b += bc * Time.deltaTime*25; }
        if (bs == false) { b -= bc * Time.deltaTime*25; }
        if (b >= .85f && bs == true) { bs = false; }
        if (b <= .05f && bs == false) { bs = true; }

        if (health <= 250 && warn != true)
        {
            warn = true;
            rc = .08f;
            gc = 0;
            bc = 0;
            b = .05f;
            g = .05f;
        }
    }


    void GameOver()
    {
        GameObject GM = GameObject.Find("_GridManager");
        GM.GetComponent<GridManager>().Restart();
    }



    //
}




/*
 *options menu:
 * turn speed
 * volume
 * zoom(or field of view)
 * key controlls
 * 
 * 
 * 
 * make a character sheet
 *
 *
 * more health
 * more control
 * more character push
 * 
 * different ships?
 *
 *
 * firing speed
 * level up 'push' to push biters.
 *
 *
 *
 * different ships?
 * melee ship?   different stats like turn speed and strafe speed?
 * 
 * 
 * */


