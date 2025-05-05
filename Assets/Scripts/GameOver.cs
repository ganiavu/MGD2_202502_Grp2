
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void QuitGameInLose()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void CreditMenu()
    {
        SceneManager.LoadScene("CreditMenu");
    }

    public void VolumeMenu()
    {
        SceneManager.LoadScene("VolumeMenu");
    }
}
