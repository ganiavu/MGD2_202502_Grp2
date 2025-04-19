using UnityEngine;

public class Rubbish : MonoBehaviour
{
    public float rotateSpeed;

    public Vector3 rotateAxis = Vector3.up;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAxis, rotateSpeed * Time.deltaTime);
    }
}
