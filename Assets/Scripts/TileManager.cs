using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    [Header("Tile Prefabs")]
    public GameObject[] tilePrefabs;          // Random tile prefabs
    public GameObject startingTilePrefab;     // First tile
    public GameObject abilityTilePrefab;      // Tile for special abilities

    [Header("Tile Spawn Settings")]
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 5;

    [Header("Player Reference")]
    public Transform playerTransform;

    [Header("Ability Tile Indexes")]
    [Tooltip("Spawn ability tile at these tile counts (e.g., 3 = 3rd tile spawned)")]
    public List<int> abilityTileIndexes = new List<int> { 3, 7, 11 };

    private List<GameObject> activeTiles = new List<GameObject>();
    private int lastRandomIndex = -1;
    private int tileCount = 0;

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnStartingTile(); // First tile
            }
            else
            {
                SpawnNextTile();
            }
        }
    }

    void Update()
    {
        if (playerTransform.position.z - 10 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnNextTile();
            DeleteTile();
        }
    }

    void SpawnNextTile()
    {
        if (abilityTileIndexes.Contains(tileCount))
        {
            SpawnAbilityTile();
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
