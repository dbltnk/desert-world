using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public GameObject PrefTile;
    public int width;
    public int height;
    public List<Tile> Tiles;
    public GameObject PrefBoundary;
    public float EvaporationRate;

    // Start is called before the first frame update
    void Start ()
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

        // ADD BOUNDARIES

        GameObject boundRight = Instantiate(PrefBoundary, transform);
        boundRight.transform.position = new Vector2(transform.position.x + width + 1f, 0);
        boundRight.GetComponent<BoxCollider>().size = new Vector3(1f, height * 2f + 1f, 1f);
        boundRight.name = "BoundRight";
        boundRight.GetComponent<Boundary>().Direction = Boundary.Directions.East;

        GameObject boundLeft = Instantiate(PrefBoundary, transform);
        boundLeft.transform.position = new Vector2(transform.position.x - width - 1f, 0);
        boundLeft.GetComponent<BoxCollider>().size = new Vector3(1f, height * 2f + 1f, 1f);
        boundLeft.name = "BoundLeft";
        boundLeft.GetComponent<Boundary>().Direction = Boundary.Directions.West;

        GameObject boundLower = Instantiate(PrefBoundary, transform);
        boundLower.transform.position = new Vector2(0, transform.position.y - height - 1f);
        boundLower.GetComponent<BoxCollider>().size = new Vector3(width * 2f + 1f, 1f, 1f);
        boundLower.name = "BoundLower";
        boundLower.GetComponent<Boundary>().Direction = Boundary.Directions.South;

        GameObject boundUpper = Instantiate(PrefBoundary, transform);
        boundUpper.transform.position = new Vector2(0, transform.position.y + height + 1f);
        boundUpper.GetComponent<BoxCollider>().size = new Vector3(width * 2f + 1f, 1f, 1f);
        boundUpper.name = "BoundUpper";
        boundUpper.GetComponent<Boundary>().Direction = Boundary.Directions.North;
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

    public void GetTileFromScreenSpace (Vector2 pos) {
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
        //if (found) return tile; else return null;

        Debug.LogError("GetTileFromScreenSpace does not really work yet.");
    }

    public Dictionary<Tile.Directions, Tile> GetNeighbours (Tile t) {
        if (t.Neighbours.Count == 0) {
            FindNeightbours(t);
        }
        return t.Neighbours;
    }

    public void FindNeightbours (Tile t) {
        Tile up = GetTileFromCoords(t.X, t.Y + 1);
        t.Neighbours.Add(Tile.Directions.North, up);

        Tile down = GetTileFromCoords(t.X, t.Y - 1);
        t.Neighbours.Add(Tile.Directions.South, down);

        Tile left = GetTileFromCoords(t.X - 1, t.Y);
        t.Neighbours.Add(Tile.Directions.West, left);

        Tile right = GetTileFromCoords(t.X + 1, t.Y);
        t.Neighbours.Add(Tile.Directions.East, right);

        Tile upright = GetTileFromCoords(t.X + 1, t.Y + 1);
        t.Neighbours.Add(Tile.Directions.NorthEast, upright);

        Tile downright = GetTileFromCoords(t.X + 1, t.Y - 1);
        t.Neighbours.Add(Tile.Directions.SouthEast, downright);

        Tile upleft = GetTileFromCoords(t.X - 1, t.Y + 1);
        t.Neighbours.Add(Tile.Directions.NorthWest, upleft);

        Tile downleft = GetTileFromCoords(t.X - 1, t.Y - 1);
        t.Neighbours.Add(Tile.Directions.SouthWest, downleft);
    }

    public void UpdateHumidityOnce() {
        foreach (Tile t in Tiles) {

            Dictionary <Tile.Directions, Tile> neighbours = GetNeighbours(t);
            float sumHumidityOfNeighbours = 0f;
            foreach (KeyValuePair<Tile.Directions, Tile> n in neighbours) {
                sumHumidityOfNeighbours += n.Value.Humidity;
            }

            int numberOfNeighbours = neighbours.Count;
            float averageHumidityOfNeighbours = sumHumidityOfNeighbours / numberOfNeighbours;

            t.Humidity = (t.Humidity + averageHumidityOfNeighbours) / 2;
            t.Humidity -= EvaporationRate;
            t.Humidity = Mathf.Clamp01(t.Humidity);
        }
    }

    public void SetupHumidity () {
        foreach (Transform c in transform) {
            Tile t = c.GetComponent<Tile>();
            if (t != null) {
                t.Humidity = Random.Range(0.98f, 1f);
                GetNeighbours(t);
            }
        }
    }
}
