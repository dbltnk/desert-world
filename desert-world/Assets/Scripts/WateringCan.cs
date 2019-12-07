using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public Map Map;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //Map.GetTileFromScreenSpace(transform.position).WaterTile();
            print("lala");
        }
    }
}
