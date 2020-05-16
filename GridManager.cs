using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    GameObject tile, map, cam, heWhoPlays;
    Canvas canvas;
    public AudioSource audioSource;
    public AudioClip pew, died;
    public Sprite spri, bullet, ship, zombie;
    public Sprite[] bgStars = new Sprite[4], bgNebula= new Sprite[3];
    float[,] Grid;
    bool inGrid, tutorialMade;
    int Vertical, Horizontal, Columns, Rows, lastx, lasty, xholder, yholder, iter, xORy, noOfNMEs;
    Transform boardholder;
    List<Vector3> gridy = new List<Vector3>();
    StartButtonSub startButton;


    private void Start()
    {
        
        cam = new GameObject("Camera");
        cam.AddComponent<AudioListener>();
        cam.AddComponent<Camera>();
        cam.GetComponent<Camera>().orthographic = true;
        cam.GetComponent<Camera>().orthographicSize = 10;
        cam.GetComponent<Camera>().backgroundColor =
            new Color(Random.Range(.1f, .15f),
            Random.Range(.28f, .33f), Random.Range(.3f, .45f), 1f);

        canvas = new GameObject("Canvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = cam.GetComponent<Camera>();

        audioSource = cam.gameObject.AddComponent<AudioSource>();

        startButton = new GameObject("StartButton").AddComponent<StartButtonSub>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        { Application.Quit(); }
    }

    public void Initiate()
    {

        //Clear our list gridPositions and reset values when restarting.
        gridy.Clear();
        tile = null;
        map = null;
        boardholder = null;
        noOfNMEs = 0;
        lastx = 0;
        lasty = 0;
        xholder = 0;
        yholder = 0;
        iter = 1;
        //∆∆∆∆∆∆ 

        cam.transform.SetPositionAndRotation(canvas.gameObject.transform.position, Quaternion.identity);
        boardholder = new GameObject("Board").transform;
        map = new GameObject("MAP");
        tile = new GameObject("tile");
        tile.transform.SetParent(map.transform);
        boardholder.SetParent(map.transform);
        Vertical = (int)cam.GetComponent<Camera>().orthographicSize;
        Horizontal = Vertical * (Screen.width / Screen.height);

        Columns = Random.Range(77,123); //warning: larger numbers become non-performant
        Rows = Random.Range(77, 123);   //100x100 = 10,000(3-5 seconds); 200x200 = 40,000(20-30 seconds & sometimes freezes)

        Grid = new float[Columns, Rows];
        for (int c = 0; c < Columns; c++)
        {
            for (int r = 0; r < Rows; r++)
            {
                Grid[c, r] = Random.Range(0.0f, 1.0f);
                SpawnTile(c, r, Grid[c, r]);
            }
        }

        
        MakeShape();
        MakePlayer();
        MakeBG();
        MakeTutorial();
    }

    void MakeTutorial()
    {
        if (tutorialMade == false)
        {
            TutorialSub tutorial = new GameObject("Tutorial").AddComponent<TutorialSub>();
            tutorial.gameObject.transform.SetParent(map.transform);
            tutorialMade = true;
        }
    }

    void MakeBG()
    {
        int ri = Random.Range(2, 5);
        for (int i = 0; i <= ri; i++)
        {
            int r = Random.Range(0, 4);
            GameObject bgs = new GameObject("BGStars"+i);
            bgs.AddComponent<SpriteRenderer>().sprite = bgStars[r];
            bgs.transform.localScale = new Vector3(2, 2, 1);
            bgs.AddComponent<Rotate>();
            bgs.GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f, .5f);
           // bgs.transform.position = GridToWorldPosition(Rows/2, Columns/2);
           // bgs.transform.Translate(0, 0, 5);
           //bgs.transform.SetParent(map.transform);
            
           //bgs.transform.SetParent(heWhoPlays.transform);
        }

        int rn = Random.Range(0, 3);
        GameObject bgn = new GameObject("BGNebula");
        bgn.AddComponent<SpriteRenderer>().sprite = bgNebula[rn];
        bgn.transform.localScale = new Vector3(2, 2, 1);
        bgn.transform.Translate(0, 0, rn+50f);
        bgn.GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f, .5f);
        bgn.AddComponent<Rotate>();
        //bgn.transform.position = GridToWorldPosition(Rows / 2, Columns / 2);
        //bgn.transform.Translate(0, 0, 10);
        //bgn.transform.SetParent(map.transform);
        //bgn.transform.SetParent(heWhoPlays.transform);
    }

    void MakePlayer()
    {
        heWhoPlays = new GameObject("Player");
        heWhoPlays.transform.position = GridToWorldPosition(2, 2);
        heWhoPlays.AddComponent<SpriteRenderer>().sprite = ship;
        heWhoPlays.transform.localScale = new Vector3(.5f, .5f, 1f);
        heWhoPlays.AddComponent<MovementSub>();
        heWhoPlays.AddComponent<CircleCollider2D>();
        heWhoPlays.AddComponent<Rigidbody2D>().gravityScale = 0;
        heWhoPlays.GetComponent<Rigidbody2D>().useAutoMass = true;
       //heWhoPlays.GetComponent<Rigidbody2D>().angularDrag = .1f;
        heWhoPlays.GetComponent<Rigidbody2D>().drag = .5f;
        heWhoPlays.GetComponent<Rigidbody2D>().freezeRotation = true;
        heWhoPlays.AddComponent<Shooting>();
        heWhoPlays.transform.parent = map.transform;
        heWhoPlays.AddComponent<PlayerSub>();

        GameObject cannon = new GameObject("Cannon");
        cannon.transform.SetParent(heWhoPlays.transform);
        cannon.transform.position = cannon.transform.parent.position;
        cannon.transform.Translate(0, 1f, 0);

        GameObject crosshair = new GameObject("Crosshair");
        crosshair.transform.SetParent(heWhoPlays.transform);
        crosshair.transform.position = crosshair.transform.parent.position;
        crosshair.transform.Translate(0, 8f, 0);
        //crosshair.AddComponent<SpriteRenderer>().sprite = bullet;
        //crosshair.transform.localScale = new Vector3(.35f, .35f, 1f);
        cam.transform.parent = heWhoPlays.transform;
        cam.transform.localPosition = new Vector3(0,10,-10);
        cam.transform.localScale = new Vector3(1,1,1);
    }

    private Vector3 GridToWorldPosition(int c, int r)
    {
        return new Vector3(c - (Horizontal - 0.5f), r - (Vertical - 0.5f), 0f);
       // return new Vector3(c - (Horizontal), r - (Vertical));
    }

    private void SpawnTile(int c, int r, float value)
    {
        GameObject go = Instantiate(tile, GridToWorldPosition(c, r),
            Quaternion.identity);
        go.AddComponent<SpriteRenderer>();
        go.name = ("X: " + c + ", Y: " + r);
        go.GetComponent<SpriteRenderer>().sprite = spri;
        go.transform.SetParent(boardholder);
        go.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, .5f);
        go.AddComponent<BoxCollider2D>();
    }

    private void SpawnNME(int c, int r, float z)
    {
        GameObject go = Instantiate(tile, GridToWorldPosition(c, r),
            Quaternion.identity);
        go.AddComponent<SpriteRenderer>().sprite = spri;
        go.name = ("X: " + c + ", Y: " + r + "Spawner");
        go.transform.SetParent(map.transform);
        go.AddComponent<SpawnSub>();
        noOfNMEs++;
    }

    private void PlaceThings()
    {
        // instantiate items and such here
        int r = gridy.Count/3;
        for (int i = 0; i <= r; i++)
        {
            //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
            int randomIndex = Random.Range(0, gridy.Count);

            //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
            Vector3 randomPosition = gridy[randomIndex];

            SpawnNME((int)randomPosition.x, (int)randomPosition.y, 0.0f);

            //Remove the entry at randomIndex from the list so that it can't be re-used.
            gridy.RemoveAt(randomIndex);
        }
    }

    private void MakeShape() //procedurally deleted tiles
    {
        if (iter >= 6)
        {
            PlaceThings();
            return;
        }

        //if (iter > 1)
        //{
        //    lastx = 0;
        //    lasty = 0;
        //    xholder = 0;
        //    yholder = 0;
        //}

        if (iter == 5)
        { iter = 6; } //exponentially larger last rooms

        // Starting Room
        int xRoom = Random.Range(5, 8);
        int yRoom = Random.Range(5, 11);

        for (int x = 1; x <= xRoom; x++)
        {
            for (int y = 1; y <= yRoom; y++)
            {
                Destroy(GameObject.Find("X: " + x + ", Y: " + y));
                // gridy.Add(new Vector3(x, y, 0f));
                xholder = x;
                yholder = y;
            }
        }

        for (int i = 0; i <= 50; i++) //50 is mostly arbitrary, I want it to continue until it goes out of bounds
        {
            int oldXorY = xORy; 

            xORy = Random.Range(0, 2);

            if (iter < 6)
            {
                // x y switch
                if (oldXorY == 0)
                { xORy = 1; }

                if (oldXorY == 1)
                { xORy = 0; }
            }

            if (xORy == 0) // horizontal corridor
            {
                int yCor = Random.Range(((yholder + 1) - yRoom), yholder);
                int corLength = Random.Range(2 + (iter * iter * 2), 4 + (iter * iter * 2));
                for (int x = 1; x <= corLength; x++)
                {
                    if ((xholder + x) >= Columns - 1 || yCor >= Rows - 1 || (xholder + x) == 0 || yCor == 0)
                    {
                        //  Debug.Log((xholder + x));
                        iter++;
                        MakeShape();
                        return;
                    }
                    Destroy(GameObject.Find("X: " + (xholder + x) + ", Y: " + yCor));
                    Destroy(GameObject.Find("X: " + (xholder + x) + ", Y: " + (yCor + 1)));
                    //gridy.Add(new Vector3((x + xholder), yCor, 0f));
                    lastx = (x + xholder);
                    lasty = yCor;
                }
            }


            if (xORy == 1) //vertical corridor
            {
                int xCor = Random.Range(((xholder + 1) - xRoom), xholder);
                int corLength = Random.Range(2 + (iter * iter * 2), 4 + (iter * iter * 2));
                for (int y = 1; y <= corLength; y++)
                {
                    if ((yholder + y) >= (Rows - 1) || xCor >= Columns - 1)
                    {
                        iter++;
                        MakeShape();
                        return;
                    }
                    Destroy(GameObject.Find("X: " + xCor + ", Y: " + (y + yholder)));
                    Destroy(GameObject.Find("X: " + (xCor + 1) +  ", Y: " + (y + yholder)));
                    // gridy.Add(new Vector3(xCor, (y + yholder), 0f));
                    lastx = xCor;
                    lasty = (yholder + y);
                }
            }


            //Room
            xRoom = Random.Range(6 + (iter * 3), 12 + (iter * 5));
            yRoom = Random.Range(6 + (iter * 3), 12 + (iter * 5));

            for (int x = 1; x <= xRoom; x++)
            {
                for (int y = 1; y <= yRoom; y++)
                {
                    if ((lasty + y - 1) >= Rows - 1 || (lastx + x - 1) >= Columns - 1)
                    {
                        iter++;
                        MakeShape();
                        return;
                    }

                    xholder = (lastx + x - 1);
                    yholder = (lasty + y - 1);

                    Destroy(GameObject.Find("X: " + (lastx + x - 1) + ", Y: " + (lasty + y - 1)));

                    //checking current vec3 against List<Vector3> gridy to prevent listing duplicate positions

                    if (x == (int)xRoom / 2 && y == (int)yRoom / 2)
                    {
                        if ((lastx + x - 1) >= 15 && (lasty + y - 1) >= 15 && gridy.Count == 0)
                        {
                            gridy.Add(new Vector3((lastx + x - 1), (lasty + y - 1), 0f));
                            //Debug.Log("first GS: " + (lastx + x - 1) + " " + (lasty + y - 1));
                        }

                        inGrid = false;

                        var vec = new Vector3((lastx + x - 1), (lasty + y - 1), 0f);

                        for (int n = 0; n < gridy.Count; n++)
                        {
                            inGrid = true;
                            inGrid &= gridy[n] == vec;
                        }
                        if ((lastx + x - 1) >= 20 && (lasty + y - 1) >= 20 && inGrid == false)
                        {
                            gridy.Add(new Vector3((lastx + x - 1), (lasty + y - 1), 0f));
                           // Debug.Log("New GS: " + (lastx + x - 1) + " " + (lasty + y - 1));
                        }
                    }
                }
            }
        }
    }

    public void NmeDestroyed()
    {
        noOfNMEs--;
        if (noOfNMEs == 0)
        { Invoke("Restart",2f); }
    }

    public void Restart()
    {
        cam.transform.parent = canvas.gameObject.transform;
        cam.transform.position = canvas.gameObject.transform.position;
        cam.transform.rotation = canvas.gameObject.transform.rotation;
        cam.transform.Translate(0, 0, -10);
        Destroy(map);
        startButton.gameObject.SetActive(true);
    }
}