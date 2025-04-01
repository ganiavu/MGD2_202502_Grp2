using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osc_LHangingShirt : MonoBehaviour
{
    public float movementSpeed = 2f; // Speed at which the obstacle falls
    public float damageAmount = 10f; // Amount of damage to inflict on collision
    private Camera mainCamera;

    private void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;

        // Start moving the obstacle downwards
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        while (true)
        {
            transform.position += Vector3.down * movementSpeed * Time.deltaTime;

            // Check if the obstacle is below the camera's view
            if (transform.position.y < mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y)
            {
                Destroy(gameObject);
                yield break; // Stop the coroutine since the object is destroyed
            }

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the character
        if (other.CompareTag("Character"))
        {
            AhGuangHealth characterHealth = other.GetComponent<AhGuangHealth>();
            if (characterHealth != null)
            {
                characterHealth.Health -= damageAmount;
                characterHealth.Health = Mathf.Clamp(characterHealth.Health, 0, characterHealth.maxHealth); // Ensure health stays within bounds
            }

            // Optionally destroy the obstacle or deactivate it
            Destroy(gameObject);
        }
    }

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool swipeDetected = false;
    private float swipeThreshold = 50f;  // Minimum swipe distance to detect a swipe
    private bool touchOnObject = false;

    public float moveSpeed = 5f;
    public float destroyDelay = 0.5f;  // Delay before destroying the prefab

    private void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                // Check if the touch is on the obstacle's collider
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    startTouchPosition = touch.position;
                    touchOnObject = true;
                    swipeDetected = false;
                }
            }
            else if (touch.phase == TouchPhase.Moved && touchOnObject)
            {
                endTouchPosition = touch.position;

                if (!swipeDetected && Vector2.Distance(startTouchPosition, endTouchPosition) > swipeThreshold)
                {
                    Vector2 swipeDirection = endTouchPosition - startTouchPosition;

                    if (swipeDirection.x < 0 && Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        swipeDetected = true;
                        StartCoroutine(MoveAndDestroy());
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Reset the touchOnObject flag when the touch ends
                touchOnObject = false;
            }
        }
    }

    private IEnumerator MoveAndDestroy()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = new Vector3(transform.position.x - 5f, transform.position.y, transform.position.z); // Move left

        while (elapsedTime < destroyDelay)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, (elapsedTime / destroyDelay));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
