﻿using System;

namespace Assets.Scrips.TopPlayers
{
    [Serializable]
    public class PlayerScore
    {
        public string name;
        public string pass;
        public int score;
        public int lastScoreInGame;

        static int SortByScore(PlayerScore p1, PlayerScore p2)
        {
            return p1.score.CompareTo(p2.score);
        }
    }
}
