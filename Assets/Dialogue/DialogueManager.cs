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
    public Image fadePanel;


    public Button nextButton;
    public Button skipButton;

    public Dialogue dialogueData;
    public string sceneToLoad;

    private int currentLine = 0;
    private Coroutine typingCoroutine;
    private bool isTyping = false;

    void Start()
    {
        nextButton.onClick.AddListener(NextClicked);
        skipButton.onClick.AddListener(SkipDialogue);
        ShowLine();
    }

    void ShowLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        DialogueLine line = dialogueData.lines[currentLine];
        characterNameText.text = line.characterName;
        characterImage.sprite = line.characterSprite;
        typingCoroutine = StartCoroutine(TypeLine(line.line));
    }

    IEnumerator TypeLine(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        isTyping = false;
    }

    public void NextClicked()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogueData.lines[currentLine].line;
            isTyping = false;
        }
        else
        {
            currentLine++;

            if (currentLine < dialogueData.lines.Length)
            {
                ShowLine();
            }
            else
            {
                EndDialogue();
            }
        }
        Debug.Log($"Clicked! currentLine = {currentLine}, total = {dialogueData.lines.Length}");

    }

    void EndDialogue()
    {
        nextButton.interactable = false;
        skipButton.interactable = false;
        StartCoroutine(FadeAndLoadScene());
    }


    void SkipDialogue()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    IEnumerator FadeAndLoadScene()
    {
        float duration = 1.5f;
        float t = 0f;
        Color color = fadePanel.color;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / duration);
            fadePanel.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }

}
