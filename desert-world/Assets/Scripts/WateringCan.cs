using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public Map Map;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            TileGetterOld tg = GetComponent<TileGetterOld>();
            tg.TileCurrent.WaterTile();
        }
    }
}
