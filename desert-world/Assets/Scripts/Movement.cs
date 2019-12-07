using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float Speed;
    public Map Map;

    public void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * Speed * Time.deltaTime;

        transform.position += tempVect;
    }

    public void WrapAround (Tile tileCurrent, Boundary boundaryCurrent) {
        Dictionary<Tile.Directions, Tile> neighbours = Map.GetNeighbours(tileCurrent);

        switch (boundaryCurrent.Direction) {
            case Boundary.Directions.East:
                foreach (KeyValuePair<Tile.Directions, Tile> n in neighbours) {
                    if (n.Key == Tile.Directions.East) TeleportToTile(n.Value);
                }
                break;
            case Boundary.Directions.West:
                foreach (KeyValuePair<Tile.Directions, Tile> n in neighbours) {
                    if (n.Key == Tile.Directions.West) TeleportToTile(n.Value);
                }
                break;
            case Boundary.Directions.South:
                foreach (KeyValuePair<Tile.Directions, Tile> n in neighbours) {
                    if (n.Key == Tile.Directions.South) TeleportToTile(n.Value);
                }
                break;
            case Boundary.Directions.North:
                foreach (KeyValuePair<Tile.Directions, Tile> n in neighbours) {
                    if (n.Key == Tile.Directions.North) TeleportToTile(n.Value);
                }
                break;
            default:
                Debug.LogError("Could not wrap around.");
                break;
        }
    }

    public void TeleportToTile (Tile t) {
        transform.position = new Vector3(t.X, t.Y, transform.position.z);
    }
}
