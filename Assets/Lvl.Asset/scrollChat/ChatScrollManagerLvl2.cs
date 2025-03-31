using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // Required for handling raycast on UI elements
using UnityEngine.SceneManagement;

public class ChatScrollManagerlvl2 : MonoBehaviour
{
    public ScrollRect scrollRect;  // Reference to the ScrollRect component
    public GameObject continueButton;  // Reference to the continue button GameObject
    public Canvas canvas;


    void Start()
    {
        // Ensure the game starts with normal time flow
        Time.timeScale = 1;

        // Hide the continue button initially
        continueButton.SetActive(false);


    }

    void Update()
    {
        // Check if the scroll is at the bottom
        if (scrollRect.verticalNormalizedPosition <= 0f)
        {
            // Pause the game when the user reaches the end of the chat
            Time.timeScale = 0;

            // Show the continue button when the chat scroll is at the bottom
            continueButton.SetActive(true);

            // Raycast for button click detection
            if (Input.GetMouseButtonDown(0))
            {
                if (IsPointerOverUIElement(continueButton))
                {
                    SceneManager.LoadScene("Level 2");
                }
            }
        }
        else
        {
            // Hide the continue button when not at the bottom
            continueButton.SetActive(false);
        }
    }

    // Function to resume the game
    //public void ResumeGame()
    //{
    //    // Resume game by setting timeScale back to 1
    //    Time.timeScale = 1;

    //    // Hide the continue button after resuming the game
    //    continueButton.SetActive(false);
    //    print("Resuming the game...");

    //    // Destroy the Canvas when resuming the game
    //    if (scrollRect != null)
    //    {
    //        Destroy(canvas.gameObject);
    //        print("Canvas destroyed");
    //    }
    //}

    // Raycast function to detect if the pointer is over the button
    private bool IsPointerOverUIElement(GameObject button)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        // Create a list to store the raycast results
        var results = new System.Collections.Generic.List<RaycastResult>();

        // Raycast against UI elements
        EventSystem.current.RaycastAll(eventData, results);

        // Check if any of the results match the button
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == button)
            {
                return true;
            }
        }

        return false;
    }
}
