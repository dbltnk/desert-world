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

    // Start is called before the first frame update
    void Start ()
    {
        GetComponent<SpriteRenderer>().sprite = spriteBarren;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
