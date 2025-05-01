using UnityEngine;

public class Bird : MonoBehaviour
{
    public GameObject spriteToSpawn1;
    public GameObject spriteToSpawn2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter(Collider collision)
    {
        // Check if the bird collides with any object that has a SpriteRenderer
        if (collision.CompareTag("Player"))
        {

            spriteToSpawn1.SetActive(true);
            spriteToSpawn2.SetActive(true);


        }
    }
}
