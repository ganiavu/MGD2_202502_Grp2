using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Slider movementSlider; // Reference to the slider in the UI
    public float movementRange = 5f; // Range of movement on the X-axis (e.g., -5 to 5)
    public float smoothSpeed = 10f; // Speed of smoothing for left-right movement
    public float forwardSpeed = 5f; // Adjustable forward movement speed
    public float speedIncreaseInterval = 30f; // Interval in seconds to increase speed
    public float speedIncrement = 0.5f; // Amount to increase forward speed

    private float targetXPosition; // Target X position for the player
    private CharacterController characterController; // CharacterController reference

    void Start()
    {
        if (movementSlider == null)
        {
            Debug.LogError("MovementSlider is not assigned!");
            return;
        }

        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController is missing on this GameObject!");
        }

        movementSlider.value = movementSlider.maxValue / 2;

        StartCoroutine(IncreaseForwardSpeed());
    }

    void Update()
    {
        if (movementSlider == null || characterController == null) return;

        float normalizedValue = (movementSlider.value - (movementSlider.maxValue / 2)) / (movementSlider.maxValue / 2);
        targetXPosition = normalizedValue * movementRange;

        float smoothedX = Mathf.Lerp(transform.position.x, targetXPosition, smoothSpeed * Time.deltaTime);

        // Only move left-right and forward — no gravity or vertical motion
        Vector3 moveDirection = new Vector3(smoothedX - transform.position.x, 0f, forwardSpeed * Time.deltaTime);
        characterController.Move(moveDirection);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle"))
        {
            PlayerManager.gameOver = true;
        }
    }

    private IEnumerator IncreaseForwardSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedIncreaseInterval);
            forwardSpeed += speedIncrement;
            Debug.Log("Forward speed increased to: " + forwardSpeed);
        }
    }
}
