using UnityEngine;

public class BirdPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform targetPoint;

    void Start()
    {
        targetPoint = pointB; // Start by moving toward point B
    }

    void Update()
    {
        // Move toward the current target point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Check if reached the target
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.05f)
        {
            // Switch target
            targetPoint = (targetPoint == pointA) ? pointB : pointA;

            // Rotate 180 degrees on Y-axis to face the other direction
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
