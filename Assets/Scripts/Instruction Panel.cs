using UnityEngine;
using UnityEngine.UI;

public class InstructionPanel : MonoBehaviour
{
    [SerializeField] private GameObject instructionPanel; // Assign your panel in the Inspector
    [SerializeField] private Button StartButton;   // Assign the button in the Inspector

    private void Start()
    {
        // Ensure the panel is active and timescale is set to 0 when the scene starts
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        // Assign the button click listener
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(ResumeGame);
        }
    }

    private void ResumeGame()
    {
        // Disable the panel and set timescale to 1
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
        }
        Time.timeScale = 1f;
    }
}