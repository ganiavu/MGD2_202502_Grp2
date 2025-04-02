using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AhGuangHealth : MonoBehaviour
{
    public float Health;
    public float maxHealth;
    public Image healthBar;
    public AhGuangHealth ahGuangHealth;

    public string retryScene;     // Scene to reload on retry
    public string mainMenuScene;  // Scene to load for main menu
    public Canvas loseCanvas;


    // Reference to the LoseHandler script
    //public LoseHandler loseHandler;  // Assign this in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        if (maxHealth <= 0)
        {
            maxHealth = 1; // Or set a default value
        }
        Time.timeScale = 1;

        Health = maxHealth; // Ensure health starts within valid bounds
        InvokeRepeating("ReduceHealthOverTime", 1f, 1f); // Repeats every second
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AmaBattery"))  // Ensure the prefab is tagged as "AmaBattery"
        {
            IncreaseHealth(20f);  // Increase health by 20
            Destroy(collision.gameObject);  // Destroy the prefab after collision
        }
    }

    void IncreaseHealth(float amount)
    {
        ahGuangHealth.Health += amount;
        ahGuangHealth.Health = Mathf.Clamp(ahGuangHealth.Health, 0, ahGuangHealth.maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();

        // Check if health is zero and trigger lose menu
        if (Health <= 0)
        {
            TriggerLoseMenu();
        }
    }

    // Method to reduce health by 1% every second
    void ReduceHealthOverTime()
    {
        if (Health > 0)
        {
            Health -= maxHealth * 0.01f; // Reduces 1% of max health
            Health = Mathf.Clamp(Health, 0, maxHealth); // Ensures health doesn't go below 0
        }

        // Check if health is zero and trigger lose menu
        if (Health <= 0)
        {
            TriggerLoseMenu();
        }
    }

    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float fillValue = Mathf.Clamp(Health / maxHealth, 0f, 1f);
            if (!float.IsNaN(fillValue) && !float.IsInfinity(fillValue))
            {
                healthBar.fillAmount = fillValue;
            }
            else
            {
                Debug.LogError("Invalid fillAmount value: " + fillValue);
            }
        }
    }

    // Method to trigger the lose menu
    void TriggerLoseMenu()
    {
        loseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RetryScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(retryScene);  // Reload the current level

    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuScene);  // Load the main menu scene

    }

}
