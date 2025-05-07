using UnityEngine;

public class shieldActivation : MonoBehaviour
{

    public GameObject Shield1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
        {

            if (Shield1 != null)
            {
                Shield1.SetActive(true);
            }
        }
    }
}
