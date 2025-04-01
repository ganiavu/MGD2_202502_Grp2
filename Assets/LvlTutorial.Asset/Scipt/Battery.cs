using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is AhGuang
        if (collision.CompareTag("Character"))
        {
            // Get the AhGuangHealth component from the collided object
            AhGuangHealth ahGuangHealth = collision.GetComponent<AhGuangHealth>();
            if (ahGuangHealth != null)
            {
                // Increase AhGuang's health by 20f
                ahGuangHealth.Health += 20f;
                ahGuangHealth.Health = Mathf.Clamp(ahGuangHealth.Health, 0f, ahGuangHealth.maxHealth);
            }

            // Destroy the Battery after increasing health
            Destroy(gameObject);
            
        }
    }
}
