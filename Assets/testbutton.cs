using UnityEngine;
using UnityEngine.UI;

public class YesButtonHandler : MonoBehaviour
{
    public Button yesButton;

    void Start()
    {
        if (yesButton != null)
        {
            yesButton.onClick.AddListener(OnYesButtonPressed);
        }
        else
        {
            Debug.LogWarning("Yes Button is not assigned in the inspector.");
        }
    }

    void OnYesButtonPressed()
    {
        Debug.Log("✅ Yes button pressed");
    }
}
