using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float delayBeforeSwitch = 30f; // Replace 30f with your desired time (xxf)
    public string nextSceneName = "NextScene"; // Set this in Inspector

    private float timer;
    public Slider progressBarSlider;

    void Update()
    {
        timer += Time.deltaTime;

        if (progressBarSlider != null)
        {
            progressBarSlider.value = Mathf.Clamp01(timer / delayBeforeSwitch);
        }

        if (timer >= delayBeforeSwitch)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
