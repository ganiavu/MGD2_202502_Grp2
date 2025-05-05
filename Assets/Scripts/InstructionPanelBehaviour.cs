using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InstructionPanelBehaviour : MonoBehaviour
{
    public GameObject instructionPanel1;
    public GameObject instructionPanel2;
    public Button startButton1;
    public Button startButton2;

    private void Start()
    {
        // Set animators to unscaled so UI can animate during pause
        SetAnimatorsToUnscaled(instructionPanel1);
        SetAnimatorsToUnscaled(instructionPanel2);

        // Show panel 1, hide panel 2
        ShowPanel(instructionPanel1, true);
        ShowPanel(instructionPanel2, false);

        // Delay pause slightly
        StartCoroutine(DelayPause());

        // Add button listeners
        startButton1.onClick.AddListener(() =>
        {
            ShowPanel(instructionPanel1, false);
            ShowPanel(instructionPanel2, true);
            UpdateGamePauseState();
        });

        startButton2.onClick.AddListener(() =>
        {
            ShowPanel(instructionPanel2, false);
            UpdateGamePauseState();
        });
    }

    IEnumerator DelayPause()
    {
        yield return new WaitForSeconds(0.1f);
        UpdateGamePauseState();
    }


    void SetAnimatorsToUnscaled(GameObject panel)
    {
        if (panel != null)
        {
            Animator[] animators = panel.GetComponentsInChildren<Animator>(true);
            foreach (var anim in animators)
            {
                anim.updateMode = AnimatorUpdateMode.UnscaledTime;
            }
        }
    }

    void ShowPanel(GameObject panel, bool show)
    {
        if (panel != null)
            panel.SetActive(show);
    }

    void UpdateGamePauseState()
    {
        bool shouldPause = (instructionPanel1 != null && instructionPanel1.activeSelf)
                        || (instructionPanel2 != null && instructionPanel2.activeSelf);

        Time.timeScale = shouldPause ? 0f : 1f;
        Debug.Log("Game paused? " + shouldPause);
    }
}
