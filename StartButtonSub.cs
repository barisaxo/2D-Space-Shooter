using UnityEngine;
using UnityEngine.UI;

public class StartButtonSub : MonoBehaviour
{
    GameObject gm;
    Text text;
    public bool starting, again;
    private void Start()
    {
        Transform canv = GameObject.Find("Canvas").transform;
        transform.SetParent(canv);
        gm = GameObject.Find("_GridManager");
        gameObject.AddComponent<RectTransform>();
        transform.position = transform.parent.position;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(2040, 2040);
        gameObject.AddComponent<SpriteRenderer>().sprite =
            gm.GetComponent<GridManager>().bgNebula[Random.Range(0,3)];
        GetComponent<SpriteRenderer>().color = new Color(.5f, .5f, .5f, .85f);
        transform.localScale = new Vector3(75, 75, 1);
        GameObject textGO = new GameObject("Tutorial Text");
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text = textGO.gameObject.AddComponent<Text>();
        text.text = "Press 'Space' To Begin";
        text.alignment = TextAnchor.MiddleCenter;
        text.resizeTextForBestFit = true;
        text.font = ArialFont;
        text.material = ArialFont.material;
        textGO.transform.SetParent(canv);
        textGO.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 200);
        textGO.transform.position = transform.parent.transform.position;
        textGO.transform.localScale = new Vector3(1, 1, 1);
        textGO.transform.SetParent(this.gameObject.transform);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        { // used this if here to give a frame so "Loading..." will appear without fail,
            //without accidentally 'double clicking' Begin();
                if (starting == false) 
            {
                starting = true;
                text.text = "Loading...";
                Invoke("Begin", Time.deltaTime);
            }
        }
    }

    void Begin()
    {
        gm.GetComponent<GridManager>().Initiate();
        starting = false;
        text.text = "Press 'Space' To Begin";
        gameObject.SetActive(false);
    }
}
