using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;      // Random tiles
    public GameObject specificTilePrefab; // Set this to the specific tile in Inspector

    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;
    private int lastRandomIndex = -1;
    private int tileCount = 0;

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0, true); // first tile always fixed
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
        if (tileCount % 2 == 0)
        {
            int randomIndex;

            // Reroll if same as last
            do
            {
                randomIndex = Random.Range(0, tilePrefabs.Length);
            }
            while (tilePrefabs.Length > 1 && randomIndex == lastRandomIndex);

            lastRandomIndex = randomIndex;
            SpawnTile(randomIndex, false);
        }
        else
        {
            SpawnSpecificTile();
        }

        tileCount++;
    }


    void SpawnTile(int tileIndex, bool isFirst = false)
    {
        Quaternion rotation = Quaternion.Euler(90f, 90f, 0f);
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    void SpawnSpecificTile()
    {
        Quaternion rotation = Quaternion.Euler(90f, 90f, 0f);
        GameObject go = Instantiate(specificTilePrefab, transform.forward * zSpawn, rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
