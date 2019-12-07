using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public Map Map;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            TileGetter tg = GetComponent<TileGetter>();
            tg.TileCurrent.WaterTile();
        }
    }
}
