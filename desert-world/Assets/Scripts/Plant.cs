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
    public float LocalHumidity = 1f;
    TileGetter tileGetter;

    void Start ()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        rend.sprite = Seed;
        Map = GameObject.Find("Map").GetComponent<Map>();
        tileGetter = GetComponent<TileGetter>();
    }

    public void Step()
    {
        Age++;
        if (Age == 4f  && LocalHumidity >= 0.5f) {
            Map.SpawnPlant(transform, SeedRange);
        }
        if (Age == 4f && LocalHumidity >= 0.6f) {
            Map.SpawnPlant(transform, SeedRange);
        }
        if (Age == 4f && LocalHumidity >= 0.7f) {
            Map.SpawnPlant(transform, SeedRange);
        }
        if (Age == 5f || LocalHumidity <= 0.05f) gameObject.SetActive(false);
        if (Age == 6f) Destroy(gameObject);
    }

    private void Update () {
        if (tileGetter != null && tileGetter.TileCurrent != null) {
            LocalHumidity = tileGetter.TileCurrent.Humidity;
        }
        if (Age == 1f && LocalHumidity >= 0.2f) rend.sprite = PlantS;
        if (Age == 2f && LocalHumidity >= 0.3f) rend.sprite = PlantM;
        if (Age == 3f && LocalHumidity >= 0.4f) rend.sprite = PlantFruited;
        if (Age == 4f || LocalHumidity < 0.15f) rend.sprite = PlantWithered;
    }
}
