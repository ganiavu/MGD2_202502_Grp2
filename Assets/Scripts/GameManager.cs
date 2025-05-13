using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTimer : MonoBehaviour
{
    public float waitTime = 3f;            // Time to wait before showing canvas
    public Slider progressSlider;          // UI Slider reference
    public GameObject canvasToShow;        // Canvas to activate after timer

    private bool coroutineStarted = false;

    void Start()
    {
        if (progressSlider != null)
        {
            progressSlider.interactable = true;
            progressSlider.value = 0f;
        }

        if (canvasToShow != null)
        {
            canvasToShow.SetActive(false); // Ensure it's hidden at start
        }
    }

    void Update()
    {
        if (Time.timeScale == 1 && !coroutineStarted)
        {
            coroutineStarted = true;
            StartCoroutine(WaitAndShowCanvas());
        }
    }

    IEnumerator WaitAndShowCanvas()
    {
        float elapsed = 0f;

        while (elapsed < waitTime)
        {
            yield return null;
            elapsed += Time.deltaTime;

            if (progressSlider != null)
            {
                progressSlider.value = Mathf.Clamp01(elapsed / waitTime);
            }
        }

        if (canvasToShow != null)
        {
            canvasToShow.SetActive(true); // ✅ Activate the canvas instead of loading a scene
        }
    }
}
