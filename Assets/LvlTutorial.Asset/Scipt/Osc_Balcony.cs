using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float damageAmount = 10f; // Amount of damage to inflict on collision

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the character
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touch");
            AhGuangHealth characterHealth = other.GetComponent<AhGuangHealth>();
            if (characterHealth != null)
            {
                characterHealth.Health -= damageAmount;
                characterHealth.Health = Mathf.Clamp(characterHealth.Health, 0, characterHealth.maxHealth); // Ensure health stays within bounds

                //  Play hurt sound
                if (AudioManager.instance != null && AudioManager.instance.ManHurt != null)
                {
                    AudioManager.instance.PlaySFX(AudioManager.instance.ManHurt);
                }
            }

            if (UIEffectsManager.Instance != null)
            {
                UIEffectsManager.Instance.TriggerRedPanel();
            }

        }
    }
}
