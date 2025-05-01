using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float followSpeed = 5f;
    public float fixedYPosition = 5f;
    public float yawAngle = 25f;
    public float rotationSpeed = 5f;

    private float currentYaw = 0f;
    private float lastPlayerX = 0f;

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned!");
            enabled = false;
            return;
        }

        lastPlayerX = player.position.x;
    }

    void LateUpdate()
    {
        // Position update
        Vector3 targetPosition = new Vector3(
            player.position.x + offset.x,
            fixedYPosition,
            player.position.z + offset.z
        );
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Detect X movement
        float deltaX = player.position.x - lastPlayerX;
        lastPlayerX = player.position.x;

        // Determine target yaw angle
        float targetYaw = 0f;
        if (Mathf.Abs(deltaX) > 0.01f)
        {
            targetYaw = Mathf.Sign(deltaX) * yawAngle;
        }

        // Smoothly interpolate yaw
        currentYaw = Mathf.Lerp(currentYaw, targetYaw, rotationSpeed * Time.deltaTime);

        // Rotate around Y-axis
        Quaternion baseRotation = Quaternion.LookRotation(player.position - transform.position);
        Quaternion yawRotation = Quaternion.Euler(0f, currentYaw, 0f);
        transform.rotation = baseRotation * yawRotation;
    }
}
