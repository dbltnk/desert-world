using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject PrefTile;
    public int width = 3;
    public int height = 2;

    // Start is called before the first frame update
    void Start()
    {
        CreateMap();
    }

    public void CreateMap() {
        for (int w = -width; w <= width; w++) {
            for (int h = -height; h <= height; h++) {
                GameObject t = Instantiate(PrefTile, transform);
                t.name = "tile " + w + " " + h;
                t.GetComponent<Tile>().X = w;
                t.GetComponent<Tile>().Y = h;
                t.transform.SetPositionAndRotation(new Vector2(w, h), Quaternion.identity);
            }
        }
    }

    public Tile[] GetNeightbours(int x, int y) {
        Tile[] found = new Tile[8];
        foreach (GameObject c in transform) {
            Tile t = c.GetComponent<Tile>();

            // up
            // down
            // left
            // right
            // upright
            // downright
            // downleft
            // upleft
        }
        return found;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
