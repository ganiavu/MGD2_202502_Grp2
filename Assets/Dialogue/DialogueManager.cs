using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections; 


public class DialogueManager : MonoBehaviour
{
    public Image background;
    public Image dialogueBox;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;

    public Button nextButton;
    public Button skipButton;

    public Dialogue dialogueData;
    public string sceneToLoad;

    private int currentLine = 0;

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private bool canClick = true;


    void Start()
    {
        ShowLine();

        // Connect button events
        nextButton.onClick.AddListener(NextLine);
        skipButton.onClick.AddListener(SkipDialogue);
    }

    void ShowLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        var line = dialogueData.lines[currentLine];
        characterNameText.text = line.characterName;
        characterImage.sprite = line.characterSprite;
        dialogueText.text = "";

        typingCoroutine = StartCoroutine(TypeLine(line.line));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.03f); // typing speed
        }

        isTyping = false;

        // Start countdown to auto-advance after full line is shown
        yield return new WaitForSeconds(5f);
        NextLine();
    }



    void EndDialogue()
    {
        // Optionally hide UI
        nextButton.gameObject.SetActive(false);
        dialogueBox.gameObject.SetActive(false);
        characterImage.gameObject.SetActive(false);
        characterNameText.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);

        // Load next scene
        SceneManager.LoadScene(sceneToLoad);
    }


    public void SkipDialogue()
    {
        SceneManager.LoadScene(sceneToLoad);
    }


    public void NextLine()
    {
        if (!canClick) return;

        if (isTyping)
        {
            // Skip typing, show full line immediately
            StopCoroutine(typingCoroutine);
            var line = dialogueData.lines[currentLine];
            dialogueText.text = line.line;
            isTyping = false;

            // Prevent auto-advance from triggering twice
            StopAllCoroutines();
            StartCoroutine(ClickCooldown());
            StartCoroutine(AutoAdvance());
            return;
        }

        currentLine++;

        if (currentLine < dialogueData.lines.Length)
        {
            ShowLine();
        }
        else
        {
            EndDialogue();
        }

        StartCoroutine(ClickCooldown());
    }

    IEnumerator AutoAdvance()
    {
        yield return new WaitForSeconds(5f);
        NextLine();
    }


    IEnumerator ClickCooldown()
    {
        canClick = false;
        yield return new WaitForSeconds(0.1f); // small delay
        canClick = true;
    }

}
