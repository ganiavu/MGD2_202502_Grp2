using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public MeshRenderer shieldMeshRenderer;  // Assign in Inspector
    public GameObject playerObject;          // Assign the player GameObject here

    private bool shieldActive = false;
    private string originalPlayerTag = "Player"; // Default tag

    void OnEnable()
    {
        shieldActive = true;

        if (shieldMeshRenderer != null)
            shieldMeshRenderer.enabled = true;

        if (playerObject != null)
            playerObject.tag = originalPlayerTag; // Ensure correct tag when starting
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!shieldActive || !other.CompareTag("Obstacle")) return;

        Debug.Log("Shield hit obstacle. Temporarily untagging player.");

        // Hide the shield visual
        if (shieldMeshRenderer != null)
            shieldMeshRenderer.enabled = false;

        // Store current tag and untag the player
        if (playerObject != null)
        {
            originalPlayerTag = playerObject.tag;
            playerObject.tag = "Untagged";
            StartCoroutine(RestorePlayerTagAfterDelay(2f));
        }

        shieldActive = false;
    }

    private IEnumerator RestorePlayerTagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (playerObject != null)
        {
            playerObject.tag = originalPlayerTag;
            Debug.Log("Player tag restored to: " + originalPlayerTag);
        }
    }
}
