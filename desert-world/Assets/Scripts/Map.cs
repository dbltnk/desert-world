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

    public List<Tile> GetNeighbours (Tile t) {
        if (t.Neighbours.Count == 0) {
            FindNeightbours(t);
        }
        return t.Neighbours;
    }

    public void FindNeightbours (Tile t) {
        List<Tile> neighbours = new List<Tile>();

        Tile up;
        if (GetTileFromCoords(t.X, t.Y + 1) == null) {
            up = GetTileFromCoords(t.X, t.Y * -1);
        }
        else {
            up = GetTileFromCoords(t.X, t.Y + 1);
        }
        neighbours.Add(up);

        Tile down;
        if (GetTileFromCoords(t.X, t.Y - 1) == null) {
            down = GetTileFromCoords(t.X, t.Y * -1);
        } else {
            down = GetTileFromCoords(t.X, t.Y - 1);
        }
        neighbours.Add(down);

        Tile left;
        if (GetTileFromCoords(t.X - 1, t.Y) == null) {
            left = GetTileFromCoords(t.X * -1, t.Y);
        } else {
            left = GetTileFromCoords(t.X - 1, t.Y);
        }
        neighbours.Add(left);

        Tile right;
        if (GetTileFromCoords(t.X + 1, t.Y) == null) {
            right = GetTileFromCoords(t.X * -1, t.Y);
        } else {
            right = GetTileFromCoords(t.X + 1, t.Y);
        }
        neighbours.Add(right);

        Tile upright;
        if (GetTileFromCoords(t.X + 1, t.Y + 1) == null) {
            // is corner tile
            if(t.X == width && t.Y == height) {
                upright = GetTileFromCoords(t.X * -1, t.Y * -1);
            }
            // is not corner tile
            else {
                upright = GetTileFromCoords(t.X + 1, t.Y * -1);
                // TODO: FIX. This does not work for most tiles, such as downright corner
            }
        } else {
            upright = GetTileFromCoords(t.X + 1, t.Y + 1);
        }
        neighbours.Add(upright);

        Tile downright;
        if (GetTileFromCoords(t.X + 1, t.Y - 1) == null) {
            // is corner tile
            if (t.X == width && t.Y == height * -1) {
                downright = GetTileFromCoords(t.X * -1, t.Y * -1);
            }
            // is not corner tile
            else {
                downright = GetTileFromCoords(t.X + 1, t.Y * -1);
            }
        } else {
            downright = GetTileFromCoords(t.X + 1, t.Y - 1);
        }
        neighbours.Add(downright);

        Tile upleft;
        if (GetTileFromCoords(t.X - 1, t.Y + 1) == null) {
            // is corner tile
            if (t.X == width * -1 && t.Y == height) {
                upleft = GetTileFromCoords(t.X * -1, t.Y * -1);
            }
            // is not corner tile
            else {
                upleft = GetTileFromCoords(t.X - 1, t.Y * -1);
            }
        } else {
            upleft = GetTileFromCoords(t.X - 1, t.Y + 1);
        }
        neighbours.Add(upleft);

        Tile downleft;
        if (GetTileFromCoords(t.X + 1, t.Y - 1) == null) {
            // is corner tile
            if (t.X == width * -1 && t.Y == height * -1) {
                downleft = GetTileFromCoords(t.X * -1, t.Y * -1);
            }
            // is not corner tile
            else {
                downleft = GetTileFromCoords(t.X - 1, t.Y * -1);
            }
        } else {
            downleft = GetTileFromCoords(t.X + 1, t.Y - 1);
        }
        neighbours.Add(upleft);

        t.Neighbours = neighbours;
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
