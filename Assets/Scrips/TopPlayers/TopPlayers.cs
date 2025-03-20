using UnityEngine;

namespace Assets.Scrips.TopPlayers
{
    public class TopPlayers : MonoBehaviour
    {
        public static TopPlayers instance;
        public PlayerScores playerScores = new PlayerScores();

        private void Awake()
        {
            if (TopPlayers.instance != null)
                Debug.LogError("UITopPlayers Error");
            TopPlayers.instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public virtual void SetTopPlayers(string jsonStringResponse)
        {
            PlayerScoresRes playerScoresRes = PlayerScoresRes.FromJSON(jsonStringResponse);
            playerScores = playerScoresRes.record;
        }
    }
}
