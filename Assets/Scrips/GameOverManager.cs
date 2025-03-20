using Assets.Scrips.TopPlayers;
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
        if (currentPlayer.score < currentScore)
        {
            currentPlayer.score = currentScore;
        }
        currentPlayer.lastScoreInGame = currentScore;
        TopPlayersUpdate.instance.GetAndUpdateTopPlayers();
        ScoreManager.Instance.ClearCurrentScore();
        TopPlayersGet.instance.GetTopPlayers();

        maxScoreUI.text = currentPlayer.score.ToString();
        currentScoreUI.text = currentScore.ToString();
    }
}
