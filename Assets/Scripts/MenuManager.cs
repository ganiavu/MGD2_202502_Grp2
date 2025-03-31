using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour
{

    public void LoadScene(string _name)
    {
        SceneManager.LoadScene(_name);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void OpenVolumeMenuFromSettings()
    {
        SceneManager.LoadScene("VolumeMenu");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
