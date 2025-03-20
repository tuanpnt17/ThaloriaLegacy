using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameManagerUI gameManager;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject mapMenu;

    public void StartGame()
    {
        gameManager.StartGame();
        SceneManager.LoadScene("Map1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        gameManager.ResumeGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
        mainMenu.SetActive(true);
        mapMenu.SetActive(false);
    }

    public void MapMenu()
    {
        mainMenu.SetActive(false);
        mapMenu.SetActive(true);
    }

    public void HelpMenu() { }
}
