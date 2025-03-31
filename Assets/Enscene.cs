using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enscene : MonoBehaviour
{
    public GameObject spritePrefab;  // Assign the sprite prefab in the inspector
    public Vector2 spawnPosition;    // Define where you want to spawn the sprite
    public float spawnDelay = 40f;   // Set the delay time as a public variable (default is 40f)

    void Start()
    {
        StartCoroutine(SpawnSpriteAfterDelayCoroutine(spawnDelay));  // Start the Coroutine with the delay
    }

    IEnumerator SpawnSpriteAfterDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified time

        // Spawn the sprite at the specified position
        Instantiate(spritePrefab, spawnPosition, Quaternion.identity);
    }
}