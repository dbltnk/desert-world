using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int X;
    public int Y;
    public Sprite spriteBarren;
    public Sprite spriteFertile;
    TextMesh debugTextPosition;
    TextMesh debugTextHumidity;
    public float Humidity;

    // Start is called before the first frame update
    void Start ()
    {
        GetComponent<SpriteRenderer>().sprite = spriteBarren;
        debugTextPosition = transform.Find("Position").GetComponentInChildren<TextMesh>();
        debugTextHumidity = transform.Find("Humidity").GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        debugTextPosition.text = $"{X}|{Y}";
        debugTextHumidity.text = Humidity.ToString("F2");
    }
}
