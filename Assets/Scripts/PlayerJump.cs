    using UnityEngine;
    using UnityEngine.UI;

public class PlayerJump : MonoBehaviour
{
    public CharacterController characterController;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController not found!");
        }

        ResetState(); // Reset player state on start
    }

    public void PerformJump()
    {
        if (characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Player jumped!");
        }
        else
        {
            Debug.Log("Cannot jump: Player is not grounded.");
        }
    }

    void Update()
    {
        // Apply gravity and reset velocity if grounded
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void ResetState()
    {
        velocity = Vector3.zero; // Reset velocity
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z); // Reset position
        Debug.Log("Player state reset after scene reload.");
    }
}