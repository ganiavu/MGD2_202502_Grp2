using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingDots : MonoBehaviour
{
    public Text loadingText; // Assign this in the Inspector
    private string[] dots = { ".", "..", "..." };
    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(AnimateDots());
        Time.timeScale = 1f;
    }

    IEnumerator AnimateDots()
    {
        while (true)
        {
            loadingText.text = dots[currentIndex];
            currentIndex = (currentIndex + 1) % dots.Length;
            yield return new WaitForSeconds(0.5f); // Adjust the speed here
        }
    }
}
