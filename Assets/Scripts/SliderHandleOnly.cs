using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderHandleOnly : MonoBehaviour, IPointerDownHandler
{
    private float firstTapTime = 0f;
    private float doubleTapThreshold = 0.4f;
    private bool isWaitingForSecondTap = false;

    public PlayerJump playerJump; // Reference to the PlayerJump script

    void Start()
    {
        if (playerJump == null)
        {
            playerJump = FindObjectOfType<PlayerJump>();
            if (playerJump == null)
            {
                Debug.LogError("PlayerJump script not found!");
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Slider tapped!");

        // Handle double-tap detection
        if (isWaitingForSecondTap)
        {
            if (Time.time - firstTapTime <= doubleTapThreshold)
            {
                Debug.Log("Double-tap detected!");
                playerJump.PerformJump(); // Trigger the jump
                isWaitingForSecondTap = false; // Reset the waiting state
            }
        }
        else
        {
            // First tap, store the time
            firstTapTime = Time.time;
            isWaitingForSecondTap = true;

            // Reset after threshold duration if second tap doesn't happen
            Invoke(nameof(ResetDoubleTap), doubleTapThreshold);
        }
    }

    private void ResetDoubleTap()
    {
        isWaitingForSecondTap = false;
    }
}