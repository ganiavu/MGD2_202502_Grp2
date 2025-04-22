using UnityEngine;
using UnityEngine.UI;

public class InstructionPanelBehaviour : MonoBehaviour
{
    public GameObject instructionPanel1;
    public GameObject instructionPanel2;
    public Button startButton1;
    public Button startButton2;

    private Animator animator1;
    private Animator animator2;

    private void Start()
    {
        // Get Animator components
        animator1 = instructionPanel1.GetComponent<Animator>();
        animator2 = instructionPanel2.GetComponent<Animator>();

        // Ensure their animations play with unscaled time
        if (animator1 != null) animator1.updateMode = AnimatorUpdateMode.UnscaledTime;
        if (animator2 != null) animator2.updateMode = AnimatorUpdateMode.UnscaledTime;

        // Pause everything
        Time.timeScale = 0f;

        // Show first panel only
        ShowPanel(instructionPanel1, true);
        ShowPanel(instructionPanel2, false);

        // Button listeners
        startButton1.onClick.AddListener(() =>
        {
            ShowPanel(instructionPanel1, false);
            ShowPanel(instructionPanel2, true);
        });

        startButton2.onClick.AddListener(() =>
        {
            ShowPanel(instructionPanel2, false);
            Time.timeScale = 1f; // Resume game
        });
    }

    void ShowPanel(GameObject panel, bool show)
    {
        if (panel != null)
            panel.SetActive(show);
    }
}
