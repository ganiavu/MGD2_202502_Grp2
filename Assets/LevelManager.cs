using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float delayBeforeSwitch = 30f; // Replace 30f with your desired time (xxf)
    public string nextSceneName = "NextScene"; // Set this in Inspector

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= delayBeforeSwitch)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
