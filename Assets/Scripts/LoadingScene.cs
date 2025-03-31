using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider progressSlider;
    public float loadingDuration = 5f; // Total duration to fill the slider

    public void LoadScene(int index)
    {
        StartCoroutine(LoadScene_Coroutine(index));
    }

    public IEnumerator LoadScene_Coroutine(int index)
    {
        progressSlider.value = 0;
        LoadingScreen.SetActive(true);

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;

        float elapsedTime = 0f;

        while (!asyncOperation.isDone)
        {
            // Calculate the percentage of time passed
            elapsedTime += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsedTime / loadingDuration);

            // Update the loading bar fill amount based on the elapsed time
            progressSlider.value = progress;

            // Allow the scene to activate when it is almost done
            if (progress >= 1f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        // Optionally, you can hide the loading screen here
        LoadingScreen.SetActive(false);
    }
}