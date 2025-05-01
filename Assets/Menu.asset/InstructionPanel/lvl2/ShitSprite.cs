using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitSprite : MonoBehaviour
{
    public BirdShitUI targetScript; // Reference to the script to monitor

    private void Start()
    {

        // Check if the targetScript is set
        if (targetScript == null)
        {
            Debug.LogError("Target script not assigned.");
            return;
        }

        // Optionally, set up a method to be called when the script is disabled
        targetScript.enabled = true; // Ensure the script is enabled at the start
    }

    private void Update()
    {
        // Check if the target script is disabled
        if (targetScript != null && !targetScript.enabled)
        {
            Destroy(gameObject); // Destroy this prefab
            Debug.Log("Target script is disabled. Destroying this prefab.");
        }
    }
}
