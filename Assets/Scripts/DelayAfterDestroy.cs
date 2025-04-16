using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    public float delay = 2f;
    public GameObject gameObject1;

    void Start()
    {
        Destroy(gameObject1, delay);
    }
}
