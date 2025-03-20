using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Scrips.TopPlayers
{
    [Serializable]
    public class PlayerScores
    {
        [SerializeField]
        public List<PlayerScore> PlayerScoresList = new List<PlayerScore>();
        public string OldMd5 = "old";
        public string NewMd5 = "new";

        //public virtual void NewPlayerScore(string name, string pass, int score)
        //{
        //    var newPlayer = new PlayerScore
        //    {
        //        name = name,
        //        pass = pass,
        //        score = score,
        //    };
        //    this.AddPlayer(newPlayer);
        //}

        public virtual void AddPlayer(PlayerScore newPlayerScore)
        {
            var exist = this.PlayerScoresList.FirstOrDefault(playerScore =>
                playerScore.name == newPlayerScore.name
            );

            if (exist == null)
            {
                this.PlayerScoresList.Add(newPlayerScore);
                return;
            }

            //if (exist.score > newPlayerScore.score)
            //    return;

            exist.score = newPlayerScore.score;
        }

        public virtual void UpdatePlayers()
        {
            this.PlayerScoresList.Sort((p1, p2) => p1.score.CompareTo(p2.score));
            this.PlayerScoresList.Reverse();

            //while (this.PlayerScoresList.Count > 5)
            //{
            //    this.PlayerScoresList.RemoveAt(5);
            //}

            var json = JsonConvert.SerializeObject(this.PlayerScoresList);
            Debug.Log("Player Score list before update::" + json);
            this.NewMd5 = this.Md5Hash(json);
        }

        public virtual bool HasUpdate()
        {
            return this.OldMd5 != this.NewMd5;
        }

        protected string Md5Hash(string input)
        {
            var hash = new StringBuilder();
            using var md5Provider = new MD5CryptoServiceProvider();
            var bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            foreach (var t in bytes)
            {
                hash.Append(t.ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
