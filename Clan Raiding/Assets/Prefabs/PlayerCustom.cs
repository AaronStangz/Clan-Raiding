using System.Collections.Generic;
using UnityEngine;

public class PlayerCustom : MonoBehaviour
{
    public GameObject HatsSpawnpoints;
    public List<GameObject> Hats;
    public float SpawnArea = 5;

    void Start()
    {
            Instantiate(Hats[Random.Range(0, Hats.Count)], HatsSpawnpoints.transform.position, HatsSpawnpoints.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
