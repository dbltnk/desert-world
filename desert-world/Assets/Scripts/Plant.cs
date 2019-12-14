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
    private float localHumidity;

    void Start ()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        rend.sprite = Seed;
        Map = GameObject.Find("Map").GetComponent<Map>();
        localHumidity = Random.Range(0f, 1f);
    }

    public void Step()
    {
        Age++;
        if (Age == 4f  && localHumidity >= 0.75f) {
            Map.SpawnPlant(transform, SeedRange);
        }
        if (Age == 4f && localHumidity >= 0.85f) {
            Map.SpawnPlant(transform, SeedRange);
        }
        if (Age == 4f && localHumidity >= 0.95f) {
            Map.SpawnPlant(transform, SeedRange);
        }
        if (Age == 5f || localHumidity <= 0.05f) gameObject.SetActive(false);
    }

    private void Update () {
        if (Age == 1f && localHumidity >= 0.25f) rend.sprite = PlantS;
        if (Age == 2f && localHumidity >= 0.5f) rend.sprite = PlantM;
        if (Age == 3f && localHumidity >= 0.75f) rend.sprite = PlantFruited;
        if (Age == 4f || localHumidity < 0.25f) rend.sprite = PlantWithered;
    }
}
