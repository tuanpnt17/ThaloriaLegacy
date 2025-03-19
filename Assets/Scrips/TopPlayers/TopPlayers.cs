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
                Debug.LogError("UTTopPlayers Error");
            TopPlayers.instance = this;
        }
    }
}
