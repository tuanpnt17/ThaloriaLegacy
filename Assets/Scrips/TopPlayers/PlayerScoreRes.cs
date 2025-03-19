using System;
using UnityEngine;

namespace Assets.Scrips.TopPlayers
{
    [Serializable]
    public class PlayerScoresRes
    {
        public PlayerScores record;

        public static PlayerScoresRes FromJSON(string jsonString)
        {
            return JsonUtility.FromJson<PlayerScoresRes>(jsonString);
        }
    }
}
