using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Collider playerCollider; // Assign this in the inspector

    private bool shieldActive = false;

    void OnEnable()
    {
        Debug.Log("Shield activated: Disabling player collider.");
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
            shieldActive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Shield hit something: " + other.name + " | Tag: " + other.tag);

        if (shieldActive && other.CompareTag("Obstacle"))
        {
            Debug.Log("Hit Obstacle! Re-enabling player collider and disabling shield.");

            StartCoroutine(EnableColliderNextFrame());

            //gameObject.SetActive(false); // This should disable the shield
        }
    }

    private IEnumerator EnableColliderNextFrame()
    {
        yield return null;
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }

        gameObject.SetActive(false);
    }
}
