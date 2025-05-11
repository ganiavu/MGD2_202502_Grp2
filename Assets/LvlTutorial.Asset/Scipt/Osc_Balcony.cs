using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osc_Balcony : MonoBehaviour
{
    //public float movementSpeed = 2f; // Speed at which the obstacle falls
    public float damageAmount = 10f; // Amount of damage to inflict on collision
    //private Camera mainCamera;

    //private void Start()
    //{
    //    // Get the main camera
    //    mainCamera = Camera.main;

    //    // Start moving the obstacle downwards
    //    //StartCoroutine(MoveDown());
    //}

    //private IEnumerator MoveDown()
    //{
    //    while (true)
    //    {
    //        //transform.position += Vector3.down * movementSpeed * Time.deltaTime;

    //        // Check if the obstacle is below the camera's view
    //        if (transform.position.y < mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y)
    //        {
    //            Destroy(gameObject);
    //            yield break; // Stop the coroutine since the object is destroyed
    //        }

    //        yield return null;
    //    }
    //}

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

            // Optionally destroy the obstacle or deactivate it
            //Destroy(gameObject);
        }
    }
}
