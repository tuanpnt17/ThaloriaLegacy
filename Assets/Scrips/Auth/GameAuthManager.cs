using System.Linq;
using Assets.Scrips.TopPlayers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAuthManager : MonoBehaviour
{
    [SerializeField]
    protected GameObject login;

    [SerializeField]
    protected GameObject register;

    [SerializeField]
    protected GameObject authMenu;

    public TMP_InputField loginPlayerName;
    public TMP_InputField loginPlayerPass;
    public TMP_InputField registerPlayerName;
    public TMP_InputField registerPlayerPass;
    public TMP_InputField registerPlayerPassConfirm;

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        login.SetActive(false);
        register.SetActive(false);
        authMenu.SetActive(true);
        TopPlayersGet.instance.Get();
    }

    public void OpenLogin()
    {
        login.SetActive(true);
        authMenu.SetActive(false);
        register.SetActive(false);
    }

    public void HandleLogin()
    {
        var playerName = loginPlayerName.text;
        var playerPass = loginPlayerPass.text;
        if (string.IsNullOrEmpty(playerName) || string.IsNullOrEmpty(playerPass))
        {
            Debug.LogWarning("Player name or password is empty");
            return;
        }

        var players = TopPlayers.instance.playerScores.PlayerScoresList;
        var player = players.SingleOrDefault(p => p.name == playerName);
        if (player == null)
        {
            Debug.LogWarning("Player not found");
            return;
        }
        if (player.pass != playerPass)
        {
            Debug.LogWarning("Password is incorrect");
            return;
        }
        Debug.Log("Login success");
        SceneManager.LoadScene("MenuScene");
    }

    public void OpenRegister()
    {
        login.SetActive(false);
        authMenu.SetActive(false);
        register.SetActive(true);
    }

    public void HandleRegister()
    {
        Debug.Log("Register");
        var playerName = registerPlayerName.text;
        var playerPass = registerPlayerPass.text;
        var playerPassConfirm = registerPlayerPassConfirm.text;
        if (
            string.IsNullOrEmpty(playerName)
            || string.IsNullOrEmpty(playerPass)
            || string.IsNullOrEmpty(playerPassConfirm)
        )
        {
            Debug.LogWarning("Player name or password is empty");
            return;
        }
        if (playerPass != playerPassConfirm)
        {
            Debug.LogWarning("Passwords do not match");
            return;
        }
        var playerScore = new PlayerScore
        {
            name = playerName,
            pass = playerPass,
            score = 0,
            lastScoreInGame = 0,
        };
        var existed = TopPlayers.instance.playerScores.PlayerScoresList.SingleOrDefault(p =>
            p.name == playerName
        );
        if (existed != null)
        {
            Debug.LogWarning("Player already exists");
            return;
        }
        TopPlayersUpdate.instance.SetCurrentPlayer(playerScore);
        TopPlayersUpdate.instance.GetAndUpdateTopPlayers();
        Debug.Log("Register success");
        OpenLogin();
    }

    public void Back()
    {
        login.SetActive(false);
        authMenu.SetActive(true);
        register.SetActive(false);
    }
}
