using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static bool gameOver;
    public GameObject gameOverPanel;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if( gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }
}
