using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerUI : MonoBehaviour
{
    [SerializeField]
    protected GameObject mainMenu;

    [SerializeField]
    protected GameObject gameOverMenu;

    [SerializeField]
    protected GameObject pauseMenu;

    [SerializeField]
    protected GameObject introduct;

    [SerializeField]
    protected GameObject mapMenu;

    [SerializeField]
    protected GameObject aboutUs;

    [SerializeField]
    protected GameObject leaderBoard;

    [SerializeField]
    protected AudioManager audioManager;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MainMenu();
        }
        else
        {
            mainMenu.SetActive(false);
        }
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        aboutUs.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGameMenu()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(true);
        mapMenu.SetActive(false);
        aboutUs.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        aboutUs.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOverMenu()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        aboutUs.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Introduct()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        introduct.SetActive(true);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        aboutUs.SetActive(false);
        Time.timeScale = 0f;
    }

    public void LeaderBoard()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        introduct.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        aboutUs.SetActive(false);
        leaderBoard.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AboutUs()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        introduct.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        aboutUs.SetActive(true);
        Time.timeScale = 0f;
    }

    public void MapMenu()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        introduct.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(true);
        aboutUs.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        aboutUs.SetActive(false);
        Time.timeScale = 1f;
        audioManager.PlayDefaultAudio();
    }
}
