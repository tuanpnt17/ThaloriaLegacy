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

    [SerializeField]
    protected GameObject popUp;

    public TMP_InputField loginPlayerName;
    public TMP_InputField loginPlayerPass;
    public TMP_InputField registerPlayerName;
    public TMP_InputField registerPlayerPass;
    public TMP_InputField registerPlayerPassConfirm;

    public TextMeshProUGUI popUpTitle;
    public TextMeshProUGUI popUpMessage;

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        login.SetActive(false);
        register.SetActive(false);
        authMenu.SetActive(true);
        popUp.SetActive(false);
    }

    public void OpenLogin()
    {
        login.SetActive(true);
        authMenu.SetActive(false);
        register.SetActive(false);
        popUp.SetActive(false);
        TopPlayersGet.instance.Get();
    }

    public void HandleLogin()
    {
        var playerName = loginPlayerName.text;
        var playerPass = loginPlayerPass.text;
        if (string.IsNullOrEmpty(playerName) || string.IsNullOrEmpty(playerPass))
        {
            Debug.LogWarning("Player name or password is empty");
            popUpTitle.text = "Error";
            popUpMessage.text = "Player name or password is empty";
            popUp.SetActive(true);
            ClearLoginTextField();
            return;
        }

        var players = TopPlayers.instance.playerScores.PlayerScoresList;
        var player = players.SingleOrDefault(p => p.name == playerName);
        if (player == null)
        {
            Debug.LogWarning("Player not found");
            popUpTitle.text = "Login failed";
            popUpMessage.text = "Player not found";
            ClearLoginTextField();
            popUp.SetActive(true);
            return;
        }
        if (player.pass != playerPass)
        {
            Debug.LogWarning("Password is incorrect");
            popUpTitle.text = "Login failed";
            popUpMessage.text = "Password is incorrect";
            ClearLoginTextField();
            popUp.SetActive(true);
            return;
        }
        Debug.Log("Login success");
        TopPlayersUpdate.instance.SetCurrentPlayer(player);
        SceneManager.LoadScene("Introduction");
    }

    private void ClearLoginTextField()
    {
        loginPlayerName.text = "";
        loginPlayerPass.text = "";
    }

    private void ClearRegisterTextField()
    {
        registerPlayerName.text = "";
        registerPlayerPass.text = "";
        registerPlayerPassConfirm.text = "";
    }

    private void ClearRegisterPassword()
    {
        registerPlayerPass.text = "";
        registerPlayerPassConfirm.text = "";
    }

    public void OpenRegister()
    {
        login.SetActive(false);
        authMenu.SetActive(false);
        register.SetActive(true);
        popUp.SetActive(false);
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
            popUpTitle.text = "Register failed";
            popUpMessage.text = "Player name or password is empty";
            popUp.SetActive(true);
            return;
        }
        if (playerPass != playerPassConfirm)
        {
            Debug.LogWarning("Passwords do not match");
            popUpTitle.text = "Register failed";
            popUpMessage.text = "Passwords do not match";
            ClearRegisterPassword();
            popUp.SetActive(true);
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
            popUpTitle.text = "Register failed";
            popUpMessage.text = "Player already exists";
            popUp.SetActive(true);
            ClearRegisterTextField();
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
        popUp.SetActive(false);
    }
}
