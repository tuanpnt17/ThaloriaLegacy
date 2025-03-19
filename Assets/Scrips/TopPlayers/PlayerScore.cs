using System;

namespace Assets.Scrips.TopPlayers
{
    [Serializable]
    public class PlayerScore
    {
        public string name;
        public string password;
        public int score;

        static int SortByScore(PlayerScore p1, PlayerScore p2)
        {
            return p1.score.CompareTo(p2.score);
        }
    }
}
