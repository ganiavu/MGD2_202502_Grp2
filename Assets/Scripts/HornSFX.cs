using UnityEngine;
using UnityEngine.UI;

public class HornSFX : MonoBehaviour
{
    public Button button1;
    public Button button2;

    private bool button1Pressed = false;
    private bool button2Pressed = false;

    private void Start()
    {
        if (button1 != null)
        {
            button1.onClick.AddListener(() =>
            {
                button1Pressed = true;
                TryPlayHornSound();
            });
        }

        if (button2 != null)
        {
            button2.onClick.AddListener(() =>
            {
                button2Pressed = true;
                TryPlayHornSound();
            });
        }
    }

    private void TryPlayHornSound()
    {
        // Only play if both buttons were pressed at least once
        if (button1Pressed && button2Pressed)
        {
            if (AudioManager.instance != null && AudioManager.instance.HornSound != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.HornSound);
                Debug.Log("[HornSFX] HornSound played after both buttons pressed.");

                // Optional: Reset flags if you want it to require both again next time
                button1Pressed = false;
                button2Pressed = false;
            }
        }
    }
}
