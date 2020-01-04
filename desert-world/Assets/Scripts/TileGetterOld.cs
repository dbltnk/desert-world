using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGetterOld : MonoBehaviour
{
    public Tile TileCurrent;
    public Boundary BoundaryCurrent;
    Movement mv;

    void Start()
    {
        mv = GetComponent<Movement>();
    }

    void Update() {
        // Bit shift the index of the layer (8) to get a bit mask
        //int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), out hit, Mathf.Infinity)) {
            //Debug.DrawRay(transform.position, transform.TransformDirection(transform.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            //Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.GetComponent<Tile>() != null) TileCurrent = hit.transform.gameObject.GetComponent<Tile>();
            if (hit.transform.GetComponent<Boundary>() != null) {
                BoundaryCurrent = hit.transform.gameObject.GetComponent<Boundary>();
                mv.WrapAround(TileCurrent, BoundaryCurrent);
                BoundaryCurrent = null;
            }    
        } else {
            //Debug.DrawRay(transform.position, transform.TransformDirection(transform.forward) * 1000, Color.blue);
            //Debug.Log("Did not Hit");
        }
    }
}
