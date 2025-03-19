using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrips.TopPlayers
{
    public class UITopPlayers : MonoBehaviour
    {
        public static UITopPlayers instance;
        public List<UIPlayer> uIPlayers = new List<UIPlayer>();

        private void Awake()
        {
            if (UITopPlayers.instance != null)
                Debug.LogError("UITopPlayers Error");
            UITopPlayers.instance = this;

            this.LoadTexts();

            TopPlayersGet.instance.Get();
        }

        protected virtual void LoadTexts()
        {
            foreach (Transform child in transform)
            {
                UIPlayer uiPlayer = child.GetComponent<UIPlayer>();
                this.uIPlayers.Add(uiPlayer);
            }
        }

        public virtual void ShowTopPlayers(string jsonStringResponse)
        {
            PlayerScoresRes playerScoresRes = PlayerScoresRes.FromJSON(jsonStringResponse);
            TopPlayers.instance.playerScores = playerScoresRes.record;

            int i = 0;
            UIPlayer uIPlayer;
            foreach (PlayerScore playerScore in TopPlayers.instance.playerScores.playerScores)
            {
                uIPlayer = this.uIPlayers[i];

                uIPlayer.playerName.text = playerScore.name;
                uIPlayer.playerScore.text = playerScore.score.ToString();
                i++;
                if (i == 5)
                    break;
            }
        }
    }
}
