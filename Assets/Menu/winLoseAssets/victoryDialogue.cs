using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryDialogue : MonoBehaviour
{
    public Image characterImage;  // UI Image for the character
    public Text dialogueText;     // UI Text for the dialogue
    public Button nextLevelButton; // The Next Level button
    public Button MenuButton;

    public string[] dialogues;    // Array of dialogue lines
    private int currentDialogueIndex = 0; // Track the current dialogue

    void Start()
    {
        // Ensure the character and dialogue UI are visible and button is disabled
        characterImage.gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        nextLevelButton.interactable = false; // Disable the button initially
        MenuButton.interactable = false;

        // Show the first dialogue line
        ShowNextDialogue();
    }

    void Update()
    {
        // Detect if the player taps the screen
        if (Input.GetMouseButtonDown(0))
        {
            ProceedDialogue();
        }
    }

    // Show the next line of dialogue
    void ShowNextDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
    }

    // Progress the dialogue or hide the character and enable the button
    void ProceedDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < dialogues.Length)
        {
            ShowNextDialogue();
        }
        else
        {
            // Hide the character and dialogue when all dialogue is finished
            characterImage.gameObject.SetActive(false);
            dialogueText.gameObject.SetActive(false);

            // Enable the Next Level button
            nextLevelButton.interactable = true;
            MenuButton.interactable = true;
        }
    }
}

