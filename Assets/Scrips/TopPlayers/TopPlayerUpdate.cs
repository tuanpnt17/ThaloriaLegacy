using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scrips.TopPlayers
{
    public class TopPlayersUpdate : MonoBehaviour
    {
        public static TopPlayersUpdate instance;
        protected TopPlayerApiCall ApiCall = new TopPlayerApiCall();
        public PlayerScore currentPlayer;

        private void Awake()
        {
            if (TopPlayersUpdate.instance != null)
                Debug.LogError("TopPlayers Error");
            TopPlayersUpdate.instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public virtual void GetAndUpdateTopPlayers()
        {
            //this.apiCall.isDebug = true;
            StartCoroutine(this.ApiCall.JsonGet(this.ApiCall.Uri(), "{}", this.OnGet2UpdateDone));
        }

        public virtual void OnGet2UpdateDone(UnityWebRequest request, string jsonStringResponse)
        {
            UnityWebRequest.Result re = request.result;
            if (re != UnityWebRequest.Result.Success)
            {
                //TODO: need more work here
                Debug.LogWarning(jsonStringResponse);
                return;
            }

            TopPlayers.instance.SetTopPlayers(jsonStringResponse);
            this.UpdateTopPlayers();
        }

        public virtual void UpdateTopPlayers()
        {
            if (currentPlayer == null)
                return;

            TopPlayers.instance.playerScores.AddPlayer(currentPlayer);
            TopPlayers.instance.playerScores.UpdatePlayers();

            if (!TopPlayers.instance.playerScores.HasUpdate())
                return;

            TopPlayers.instance.playerScores.OldMd5 = TopPlayers.instance.playerScores.NewMd5;
            string json = JsonConvert.SerializeObject(TopPlayers.instance.playerScores);
            StartCoroutine(this.ApiCall.JsonPut(this.ApiCall.Uri(), json, this.OnUpdateDone));
        }

        public virtual void OnUpdateDone(UnityWebRequest request, string jsonStringResponse)
        {
            UnityWebRequest.Result re = request.result;
            if (re != UnityWebRequest.Result.Success)
            {
                //TODO: need more work here
                Debug.LogWarning(jsonStringResponse);
                return;
            }

            Debug.Log("Get top players after update:: " + jsonStringResponse);
            TopPlayers.instance.SetTopPlayers(jsonStringResponse);
        }

        public virtual void SetCurrentPlayer(string playerName, string playerPass, int playerScore)
        {
            this.currentPlayer = new PlayerScore
            {
                name = playerName,
                pass = playerPass,
                score = playerScore,
            };
        }

        public virtual void SetCurrentPlayer(PlayerScore playerScore)
        {
            this.currentPlayer = playerScore;
        }
    }
}
