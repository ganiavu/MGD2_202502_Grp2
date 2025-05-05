using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    [Header("Tile Prefabs")]
    public GameObject[] tilePrefabs;         // Random tiles
    public GameObject startingTilePrefab;    // First tile
    public GameObject abilityTilePrefab;     // Ability tile

    [Header("Tile Spawn Settings")]
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 5;

    [Header("Player Reference")]
    public Transform playerTransform;

    [Header("Ability Tile Timers (seconds)")]
    [Tooltip("Ability tile will spawn when game time reaches these seconds")]
    public List<float> abilitySpawnTimes = new List<float> { 15f, 30f, 60f };

    private List<GameObject> activeTiles = new List<GameObject>();
    private int lastRandomIndex = -1;
    private int tileCount = 0;
    private bool spawnAbilityNext = false;

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnStartingTile();
            }
            else
            {
                SpawnNextTile();
            }
        }
    }

    void Update()
    {
            Debug.Log("Current Time.timeScale = " + Time.timeScale);
        
        // Check if it's time to spawn an ability tile
        if (abilitySpawnTimes.Count > 0 && Time.time >= abilitySpawnTimes[0])
        {
            spawnAbilityNext = true;
            abilitySpawnTimes.RemoveAt(0);
        }

        if (playerTransform.position.z - 10 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnNextTile();
            DeleteTile();
        }
    }

    void SpawnNextTile()
    {
        if (spawnAbilityNext)
        {
            SpawnAbilityTile();
            spawnAbilityNext = false;
        }
        else
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(0, tilePrefabs.Length);
            }
            while (tilePrefabs.Length > 1 && randomIndex == lastRandomIndex);

            lastRandomIndex = randomIndex;
            SpawnRandomTile(randomIndex);
        }

        tileCount++;
    }

    void SpawnStartingTile()
    {
        Quaternion rotation = Quaternion.Euler(90f, 90f, 0f);
        GameObject go = Instantiate(startingTilePrefab, transform.forward * zSpawn, rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    void SpawnRandomTile(int tileIndex)
    {
        Quaternion rotation = Quaternion.Euler(90f, 90f, 0f);
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    void SpawnAbilityTile()
    {
        Quaternion rotation = Quaternion.Euler(90f, 90f, 0f);
        GameObject go = Instantiate(abilityTilePrefab, transform.forward * zSpawn, rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
