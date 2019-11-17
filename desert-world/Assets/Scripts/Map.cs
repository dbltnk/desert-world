using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map : MonoBehaviour
{
    public GameObject PrefTile;
    public int width;
    public int height;
    public List<Tile> Tiles;

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
        foreach (Transform c in transform) {
            Tile t = c.GetComponent<Tile>();
            Tiles.Add(t);
        }
    }

    Tile GetTileFromCoords(int x, int y) {

        while (x > width) x -= 2 * width + 1;
        while (x < width * -1) x += 2 * width + 1;
        while (y > height) y -= 2 * height + 1;
        while (y < height * -1) y += 2 * height + 1;

        Tile tile = null;
        bool found = false;
        foreach (Tile t in Tiles) {
            if (t.X == x && t.Y == y) {
                tile = t;
                found = true;
            }
        }
        if (found) return tile; else return null;
    }

    Vector2Int GetCoordsFromTile(Tile t) {
        return new Vector2Int(t.X, t.Y);
    }

    public Tile GetTileFromScreenSpace (Vector2 pos) {
        Tile tile = null;
        bool found = false;

        float xMin = 999999f;
        float xMax = 0f;
        float yMin = 999999f;
        float yMax = 0f;

        foreach (RectTransform t in transform) {
            if (t.anchoredPosition.x < xMin) xMin = t.anchoredPosition.x;
            if (t.anchoredPosition.x > xMax) xMax = t.anchoredPosition.x;
            if (t.anchoredPosition.y < yMin) yMin = t.anchoredPosition.y;
            if (t.anchoredPosition.y > yMax) yMax = t.anchoredPosition.y;
        }

        float x = UTK.Generic.MapIntoRange(pos.x, xMin, width * -1f, xMax, width);
        float y = UTK.Generic.MapIntoRange(pos.y, yMin, yMax, height * -1f, height);

        foreach (Tile t in Tiles) {
            if (t.X ==x && t.Y == y) {
                tile = t;
                found = true;
            }
        }
        if (found) return tile; else return null;
    }

    public List<Tile> GetNeighbours (Tile t) {
        if (t.Neighbours.Count == 0) {
            FindNeightbours(t);
        }
        return t.Neighbours;
    }

    public void FindNeightbours (Tile t) {
        Tile up = GetTileFromCoords(t.X, t.Y + 1);
        t.Neighbours.Add(up);

        Tile down = GetTileFromCoords(t.X, t.Y - 1);
        t.Neighbours.Add(down);

        Tile left = GetTileFromCoords(t.X - 1, t.Y);
        t.Neighbours.Add(left);

        Tile right = GetTileFromCoords(t.X + 1, t.Y);
        t.Neighbours.Add(right);

        Tile upright = GetTileFromCoords(t.X + 1, t.Y + 1);
        t.Neighbours.Add(upright);

        Tile downright = GetTileFromCoords(t.X + 1, t.Y - 1);
        t.Neighbours.Add(downright);

        Tile upleft = GetTileFromCoords(t.X - 1, t.Y + 1);
        t.Neighbours.Add(upleft);

        Tile downleft = GetTileFromCoords(t.X + 1, t.Y - 1);
        t.Neighbours.Add(upleft);
    }

    public void UpdateHumidityOnce() {
        foreach (Tile t in Tiles) {
            List<Tile> neighbours = GetNeighbours(t);
            float sumHumidityOfNeighbours = 0f;
            for (int i = 0; i < neighbours.Count; i++) {
                if (neighbours[i] != null) {
                    sumHumidityOfNeighbours += neighbours[i].Humidity;
                }
            }

            int numberOfNeighbours = neighbours.Where(n => n != null).Count();
            float averageHumidityOfNeighbours = sumHumidityOfNeighbours / numberOfNeighbours;

            t.Humidity -= (1 - averageHumidityOfNeighbours);
            t.Humidity = Mathf.Clamp01(t.Humidity);
        }
    }

    public void SetupHumidity () {
        foreach (Transform c in transform) {
            Tile t = c.GetComponent<Tile>();
            t.Humidity = Random.Range(0.98f, 1f);
            FindNeightbours(t);
        }
    }
}
