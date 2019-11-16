using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int X;
    public int Y;
    TextMesh debugTextPosition;
    TextMesh debugTextHumidity;
    public float Humidity;
    SpriteRenderer rend;
    public List<Tile> Neighbours;

    // Start is called before the first frame update
    void Start ()
    {
        rend = transform.Find("Fertile").GetComponent<SpriteRenderer>();
        debugTextPosition = transform.Find("Position").GetComponentInChildren<TextMesh>();
        debugTextHumidity = transform.Find("Humidity").GetComponentInChildren<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        debugTextPosition.text = $"{X}|{Y}";
        debugTextHumidity.text = Humidity.ToString("F2");
        Color c = rend.color;
        rend.color = new Color(c.r, c.g, c.b, Humidity);
    }

    public void WaterTile() {
        Humidity = 1f;
    }
}
