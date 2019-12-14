using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plant : MonoBehaviour
{
    public Sprite Seed;
    public Sprite PlantS;
    public Sprite PlantM;
    public Sprite PlantFruited;
    public Sprite PlantWithered;
    private SpriteRenderer rend;
    public float Age = 0f;
    public Map Map;
    public float SeedRange;

    void Start ()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        rend.sprite = Seed;
        Map = GameObject.Find("Map").GetComponent<Map>();
    }

    public void Step()
    {
        Age++;
        if (Age == 4f) {
            Map.SpawnPlant(transform, SeedRange);
            //Map.SpawnPlant(transform, SeedRange);
            //Map.SpawnPlant(transform, SeedRange);
        }
        if (Age == 5f) Destroy(gameObject);
    }

    private void Update () {
        if (Age == 1f) rend.sprite = PlantS;
        if (Age == 2f) rend.sprite = PlantM;
        if (Age == 3f) rend.sprite = PlantFruited;
        if (Age == 4f) {
            rend.sprite = PlantWithered;
        }
    }
}
