using UnityEngine;

public class Rotate : MonoBehaviour
{
    float rotation, value, adjust;
    SpriteRenderer sR;
    bool switching;
    GameObject anchor;
    //Transform playerTF;

    void Start()
    {
        anchor = GameObject.Find("Crosshair");
       // playerTF = player.transform;
        sR = GetComponent<SpriteRenderer>();

        value = Random.Range(.15f, .55f);
        adjust = Random.Range(.0001f, .0008f);

        rotation = Random.Range(.5f, 1.5f);

        int invertR = Random.Range(0, 2);
        if (invertR == 0) { rotation *= -1; }

        transform.Rotate(0, 0, Random.Range(-360f, 360f));
    }


    void Update()
    {
        transform.Rotate(0, 0, rotation * Time.deltaTime);

        sR.color = new Color(value, value, value, value+.15f);
        ColorValue(value);

        transform.position = Vector3.MoveTowards(transform.position, anchor.transform.position, 1);
    }





    void ColorValue(float newValue)
    {
        if (newValue <= .15f) { switching = false; }
        if (newValue >= .55f) { switching = true; }

        if (switching == true)  { value -= adjust; }
        if (switching == false) { value += adjust; }
    }
}