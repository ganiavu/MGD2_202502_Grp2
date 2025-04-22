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
        // Ensure all animators inside panels play with unscaled time
        SetAnimatorsToUnscaled(instructionPanel1);
        SetAnimatorsToUnscaled(instructionPanel2);

        // Show only panel 1 at start, pause game
        ShowPanel(instructionPanel1, true);
        ShowPanel(instructionPanel2, false);
        UpdateGamePauseState();

        // Button listeners
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
