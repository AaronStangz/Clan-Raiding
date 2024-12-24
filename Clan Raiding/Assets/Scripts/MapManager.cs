using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class MapManager : MonoBehaviour
{
    public List<GameObject> Spawnpoints; // Points on the map where items will be spawned
    public List<GameObject> Spawning;    // Prefabs to spawn (must have Network Identity)
    public float SpawnArea = 5f;         // Random +/- range around each spawnpoint

    [ServerCallback] // Ensures this method runs only on the server
    void Start()
    {
        // Loop through each spawnpoint
        foreach (GameObject point in Spawnpoints)
        {
            // Random offset within SpawnArea
            Vector3 randomOffset = new Vector3(
                Random.Range(-SpawnArea, SpawnArea),
                0f,
                Random.Range(-SpawnArea, SpawnArea)
            );

            // Random rotation around the Y-axis
            Vector3 randomRot = new Vector3(0f, Random.Range(0f, 360f), 0f);

            // Choose a random prefab from the Spawning list
            GameObject prefabToSpawn = Spawning[Random.Range(0, Spawning.Count)];

            // Instantiate the prefab on the server
            GameObject spawnedObject = Instantiate(
                prefabToSpawn,
                point.transform.position + randomOffset,
                Quaternion.Euler(randomRot)
            );

            // Spawn the object over the network
            NetworkServer.Spawn(spawnedObject);
        }
    }
}
