using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager

public class PauseScreenBehaviour : MainMenuBehaviour
{
    public static bool paused;

    [Tooltip("Reference to the pause menu object to turn on / off")]
    public GameObject pauseMenu;

    [Tooltip("Reference to the on screen controls menu")]
    public GameObject onScreenControls;

    [Tooltip("Reference to the volume menu")]
    public GameObject volumeMenu;

    /// <summary>
    /// Reloads our current level, effectively "restarting" the game
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Will turn our pause menu on or off
    /// </summary>
    /// <param name="isPaused"></param>
    public void SetPauseMenu(bool isPaused)
    {
        paused = isPaused;
        Time.timeScale = (paused) ? 0 : 1;

        pauseMenu.SetActive(paused);
        onScreenControls.SetActive(!paused);

        // Always hide volume menu when pause is toggled
        if (!isPaused && volumeMenu != null)
        {
            volumeMenu.SetActive(false);
        }
    }

    /// <summary>
    /// Toggles the volume menu and hides pause menu
    /// </summary>
    public void SetVolumeMenu(bool isActive)
    {
        volumeMenu.SetActive(isActive);
        pauseMenu.SetActive(!isActive); // Hide pause menu if volume menu is open
    }

    void Start()
    {
        SetPauseMenu(false);
    }
}
