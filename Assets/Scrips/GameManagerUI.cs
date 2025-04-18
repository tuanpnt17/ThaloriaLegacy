﻿using Assets.Scrips.TopPlayers;
using TMPro;
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

    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerScore;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            MainMenu();
        }
        else
        {
            mainMenu.SetActive(false);
        }

        var currentPlayer = TopPlayersUpdate.instance.currentPlayer;
        playerName.text = currentPlayer.name;
        playerScore.text = ScoreManager.Instance.GetCurrentScore().ToString();
    }

    private void Update()
    {
        if (playerScore == null)
            return;
        playerScore.text = ScoreManager.Instance.GetCurrentScore().ToString();
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        leaderBoard.SetActive(false);
        introduct.SetActive(false);
        aboutUs.SetActive(false);
        Time.timeScale = 1f;
        ScoreManager.Instance.ClearCurrentScore();
    }

    public void Logout()
    {
        TopPlayersUpdate.instance.SetCurrentPlayer(null);
        SceneManager.LoadScene("AuthScene");
    }

    public void PauseGameMenu()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(true);
        mapMenu.SetActive(false);
        leaderBoard.SetActive(false);
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
        leaderBoard.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GameOverMenu()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        aboutUs.SetActive(false);
        leaderBoard.SetActive(false);
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
        leaderBoard.SetActive(false);
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
        TopPlayersGet.instance.GetTopPlayers();
    }

    public void AboutUs()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        introduct.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        aboutUs.SetActive(true);
        leaderBoard.SetActive(false);
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
        leaderBoard.SetActive(false);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        aboutUs.SetActive(false);
        leaderBoard.SetActive(false);
        Time.timeScale = 1f;
    }
}
