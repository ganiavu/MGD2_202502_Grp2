using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTimer : MonoBehaviour
{
    public float waitTime;            // Time to wait before switching scene
    public string sceneToLoad;             // Scene name
    public Slider progressSlider;          // UI Slider reference

    private bool coroutineStarted = false;

    void Start()
    {
        if (progressSlider != null)
        {
            progressSlider.interactable = false;
            progressSlider.value = 0f;
        }
    }

    void Update()
    {
        if (Time.timeScale == 1 && !coroutineStarted)
        {
            coroutineStarted = true;
            StartCoroutine(WaitAndLoadScene());
        }
    }

    IEnumerator WaitAndLoadScene()
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

        SceneManager.LoadScene(sceneToLoad);
    }
}
