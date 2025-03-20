using Assets.Scrips.TopPlayers;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI maxScoreUI;
    public TextMeshProUGUI currentScoreUI;

    public void Awake()
    {
        var currentPlayer = TopPlayersUpdate.instance.currentPlayer;
        var currentScore = ScoreManager.Instance.GetCurrentScore();
        Debug.Log("GameOverManager Awake::" + currentScore);
        if (currentPlayer.score < currentScore)
        {
            currentPlayer.score = currentScore;
        }
        var playerJson = JsonConvert.SerializeObject(currentPlayer);
        currentPlayer.lastScoreInGame = currentScore;

        Debug.Log("Current Player:: " + playerJson);
        TopPlayersUpdate.instance.GetAndUpdateTopPlayers();
        ScoreManager.Instance.ClearCurrentScore();

        maxScoreUI.text = currentPlayer.score.ToString();
        currentScoreUI.text = currentScore.ToString();
    }
}
