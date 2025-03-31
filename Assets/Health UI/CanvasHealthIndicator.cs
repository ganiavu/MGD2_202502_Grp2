using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasHealthIndicator : MonoBehaviour
{
    public Image HealthBar; // Reference to the health bar image
    public GameObject lowHealthPrefab; // Prefab to be added when health is below 50%
    public Text healthPercentageText;  // Reference to the text displaying the percentage
    private GameObject instantiatedPrefab; // Reference to the instantiated prefab

    // Update is called once per frame
    void Update()
    {
        CheckHealthBar();
        UpdateHealthPercentage();
    }

    // Check the health bar fill amount to add/remove prefab
    void CheckHealthBar()
    {
        if (HealthBar == null)
        {
            Debug.LogError("HealthBar is not assigned.");
            return;
        }

        if (HealthBar.fillAmount < 0.5f)
        {
            if (instantiatedPrefab == null)
            {
                // Instantiate the prefab when health is below 50%
                instantiatedPrefab = Instantiate(lowHealthPrefab, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (instantiatedPrefab != null)
            {
                // Destroy the prefab when health is above 50%
                Destroy(instantiatedPrefab);
                instantiatedPrefab = null;
            }
        }
    }

    // Update the health percentage text in the middle of the health bar
    void UpdateHealthPercentage()
    {
        if (HealthBar != null && healthPercentageText != null)
        {
            // Convert fillAmount (0 to 1) to a percentage (0% to 100%)
            int healthPercentage = Mathf.RoundToInt(HealthBar.fillAmount * 100);
            // Update the text to show the percentage
            healthPercentageText.text = healthPercentage + "%";
        }
    }
}
