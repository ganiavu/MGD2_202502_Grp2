using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderDetector : MonoBehaviour,IPointerDownHandler
{
    public Slider movementSlider; // Reference to the slider in the UI
    public PlayerJump playerJump; // Reference to the PlayerJump script

    private float firstTapTime = 0f; // Time of the first tap
    private float doubleTapThreshold = 0.4f; // Maximum time between taps to register a double-tap
    private bool isWaitingForSecondTap = false; // Tracks if we're waiting for a second tap

    void Start()
    {
        if (movementSlider == null)
        {
            Debug.LogError("MovementSlider is not assigned!");
        }

        if (playerJump == null)
        {
            Debug.LogError("PlayerJump script is not assigned!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Slider handle tapped!");

        if (isWaitingForSecondTap)
        {
            // Check if the second tap is within the double-tap threshold
            if (Time.time - firstTapTime <= doubleTapThreshold)
            {
                // It's a valid double-tap
                Debug.Log("Double-tap detected!");
                if (playerJump != null)
                {
                    playerJump.PerformJump(); // Call the jump function
                }
                isWaitingForSecondTap = false; // Reset the flag
            }
        }
        else
        {
            // First tap
            firstTapTime = Time.time;
            isWaitingForSecondTap = true;

            // Reset the flag after the threshold duration to prevent false positives
            Invoke(nameof(ResetDoubleTap), doubleTapThreshold);
        }
    }

    private void ResetDoubleTap()
    {
        isWaitingForSecondTap = false;
    }
}