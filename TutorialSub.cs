using UnityEngine;
using UnityEngine.UI;

public class TutorialSub : MonoBehaviour
{
    Text text;
    float timer;

    void Start()
    {
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        text = gameObject.AddComponent<Text>();
        text.alignment = TextAnchor.MiddleLeft;
        text.resizeTextForBestFit = true;
        text.font = ArialFont;
        text.material = ArialFont.material;
        text.transform.SetParent(GameObject.Find("Canvas").transform);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 400);
        transform.localScale = new Vector3(1, 1, 1);
        transform.localPosition = new Vector3(0, 0, 0);
        timer = 30;
        Destroy(gameObject, 30f);
    }

    
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        { Destroy(gameObject, Time.deltaTime); }
        
        timer -= Time.deltaTime;
        
        text.text =
            "For Movement: W,A,S,D \nTo Strafe: F,G \n \n" +
            "To Shoot: <color=red>J = RED,</color> " +
            "<color=green>K = GREEN,</color> " +
            "<color=blue>L = BLUE,</color> \n" +
            "<color=yellow>U = YELLOW,</color> " +
            "<color=magenta>I = MAGENTA,</color> " +
            "<color=cyan>O = CYAN,</color> " +
            "<color=white>'Space' = WHITE.</color> \n \n" +
            "'Esc' = QUIT GAME. \n \n" +
            "Kill the enemy Spawners by hitting them 5 times. \n" +
            "Kill the enemy Biters by hitting them with the appropriate color. \n \n" +
            "Press 'Tab' to continue. \n" +
            "This message will self destruct in " + (int)timer + " seconds";
    }
}
