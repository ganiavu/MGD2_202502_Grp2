using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIEffectsManager : MonoBehaviour
{
    public static UIEffectsManager Instance;

    public Image greenPanel;
    public Image redPanel;
    public float fadeDuration = 1f;

    private Coroutine greenRoutine;
    private Coroutine redRoutine;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void TriggerGreenPanel()
    {
        if (greenRoutine != null)
            StopCoroutine(greenRoutine);

        greenRoutine = StartCoroutine(FadePanel(greenPanel, 0.7f));
    }

    public void TriggerRedPanel()
    {
        if (redRoutine != null)
            StopCoroutine(redRoutine);

        redRoutine = StartCoroutine(FadePanel(redPanel, 0.7f));
    }

    IEnumerator FadePanel(Image panel, float startAlpha)
    {
        panel.gameObject.SetActive(true);
        Color color = panel.color;
        color.a = startAlpha;
        panel.color = color;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0f, t / fadeDuration);
            panel.color = color;
            yield return null;
        }

        color.a = 0f;
        panel.color = color;
        panel.gameObject.SetActive(false);
    }
}
