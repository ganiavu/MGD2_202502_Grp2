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
        Time.timeScale = 0f;
        // Show first panel and pause game
        ShowPanel(instructionPanel1, true);
        ShowPanel(instructionPanel2, false);
        PauseGame(true);

        // Button listeners
        startButton1.onClick.AddListener(() =>
        {
            ShowPanel(instructionPanel1, false);
            ShowPanel(instructionPanel2, true);
            PauseGame(true); // still paused
        });

        startButton2.onClick.AddListener(() =>
        {
            ShowPanel(instructionPanel2, false);
            PauseGame(false); // resume
        });
    }

    void PauseGame(bool isPaused)
    {
        Time.timeScale = isPaused ? 0f : 1f;
        Debug.Log("Game paused? " + isPaused);
    }

    void ShowPanel(GameObject panel, bool show)
    {
        if (panel != null)
            panel.SetActive(show);
    }
}
