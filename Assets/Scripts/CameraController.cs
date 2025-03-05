using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public Vector3 offset = new Vector3(0, 5, -10); // Offset between the camera and player
    public float followSpeed = 5f; // Speed at which the camera follows the player
    public float fixedYPosition = 5f; // Fixed Y-axis value for the camera

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned!");
            return;
        }

        // Target position for the camera, with a fixed Y position
        Vector3 targetPosition = new Vector3(
            player.position.x + offset.x,
            fixedYPosition, // Keep Y-axis fixed
            player.position.z + offset.z
        );

        // Smoothly move the camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Optional: Align the camera's rotation (if needed)
        transform.LookAt(player);
    }
}