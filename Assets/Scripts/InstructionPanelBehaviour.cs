using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionPanelBehaviour : MonoBehaviour
{
    public GameObject instructionPanel1;
    public GameObject instructionPanel2;
    public GameObject instructionPanel3; // Optional third panel (only in Level 1)

    public Button startButton1;
    public Button startButton2;
    public Button startButton3; // Optional third button (only in Level 1)

    private bool isLevel1;

    private void Start()
    {
        isLevel1 = SceneManager.GetActiveScene().name == "Level 1";

        SetAnimatorsToUnscaled(instructionPanel1);
        SetAnimatorsToUnscaled(instructionPanel2);
        if (isLevel1) SetAnimatorsToUnscaled(instructionPanel3);

        // Show first panel, hide the rest
        ShowPanel(instructionPanel1, true);
        ShowPanel(instructionPanel2, false);
        if (isLevel1) ShowPanel(instructionPanel3, false);

        StartCoroutine(DelayPause());

        startButton1.onClick.AddListener(() =>
        {
            ShowPanel(instructionPanel1, false);
            ShowPanel(instructionPanel2, true);
            UpdateGamePauseState();
        });

        startButton2.onClick.AddListener(() =>
        {
            ShowPanel(instructionPanel2, false);
            if (isLevel1)
            {
                ShowPanel(instructionPanel3, true);
            }
            else
            {
                UpdateGamePauseState();
            }
        });

        if (isLevel1 && startButton3 != null)
        {
            startButton3.onClick.AddListener(() =>
            {
                ShowPanel(instructionPanel3, false);
                UpdateGamePauseState();
            });
        }
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
                        || (instructionPanel2 != null && instructionPanel2.activeSelf)
                        || (isLevel1 && instructionPanel3 != null && instructionPanel3.activeSelf);

        Time.timeScale = shouldPause ? 0f : 1f;
        Debug.Log("Game paused? " + shouldPause);
    }
}
