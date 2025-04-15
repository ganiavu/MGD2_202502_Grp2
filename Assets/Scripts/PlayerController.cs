using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public Slider movementSlider;
    public float movementRange = 5f;
    public float smoothSpeed = 10f;
    public float forwardSpeed = 5f;
    public float speedIncreaseInterval = 30f;
    public float speedIncrement = 0.5f;

    [Header("Tilt Settings")]
    public float tiltAngle = 20f;    // Max tilt angle on Z
    public float tiltSpeed = 5f;     // Tilt smoothing speed
    public float idleDelay = 0.2f;   // Delay before returning to Z = 0

    private float targetXPosition;
    private CharacterController characterController;

    // Internal tilt tracking
    private float previousSliderValue = -1f;
    private float sliderIdleTime = 0f;

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

        float currentSliderValue = movementSlider.value;
        float normalizedValue = (currentSliderValue - (movementSlider.maxValue / 2)) / (movementSlider.maxValue / 2);
        targetXPosition = normalizedValue * movementRange;

        float smoothedX = Mathf.Lerp(transform.position.x, targetXPosition, smoothSpeed * Time.deltaTime);
        Vector3 moveDirection = new Vector3(smoothedX - transform.position.x, 0f, forwardSpeed * Time.deltaTime);
        characterController.Move(moveDirection);

        // Check if the slider moved
        if (Mathf.Abs(currentSliderValue - previousSliderValue) < 0.0001f)
        {
            sliderIdleTime += Time.deltaTime;
        }
        else
        {
            sliderIdleTime = 0f;
        }

        bool shouldReturnToNeutral = sliderIdleTime >= idleDelay;
        ApplyTilt(normalizedValue, shouldReturnToNeutral);

        previousSliderValue = currentSliderValue;
    }

    private void ApplyTilt(float input, bool returnToNeutral)
    {
        float targetZ = 0f;
        float targetY = transform.eulerAngles.y;
        float targetX = 73.6f; // Always lock X

        if (!returnToNeutral)
        {
            targetZ = -input * tiltAngle;
        }

        Quaternion targetRotation = Quaternion.Euler(targetX, targetY, targetZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
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
