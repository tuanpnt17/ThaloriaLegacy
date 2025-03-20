using Assets.Scrips.TopPlayers;
using UnityEngine;
using UnityEngine.Networking;

public class TopPlayersGet : MonoBehaviour
{
    public static TopPlayersGet instance;
    protected TopPlayerApiCall apiCall = new TopPlayerApiCall();

    private void Awake()
    {
        if (TopPlayersGet.instance != null)
        {
            Debug.LogError("TopPlayers Error");
            return;
        }
        TopPlayersGet.instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public virtual void Get()
    {
        Debug.Log("Get top players");
        StartCoroutine(this.apiCall.JsonGet(this.apiCall.Uri(), "{}", this.JustGet));
    }

    public virtual void JustGet(UnityWebRequest request, string jsonStringResponse)
    {
        UnityWebRequest.Result re = request.result;
        if (re != UnityWebRequest.Result.Success)
        {
            //TODO: need more work here
            Debug.LogWarning(jsonStringResponse);
            return;
        }
        TopPlayers.instance.SetTopPlayers(jsonStringResponse);
        Debug.Log(jsonStringResponse);
    }

    public virtual void GetTopPlayers()
    {
        StartCoroutine(this.apiCall.JsonGet(this.apiCall.Uri(), "{}", this.OnGetTopPlayersDone));
    }

    public virtual void OnGetTopPlayersDone(UnityWebRequest request, string jsonStringResponse)
    {
        UnityWebRequest.Result re = request.result;
        if (re != UnityWebRequest.Result.Success)
        {
            //TODO: need more work here
            Debug.LogWarning(jsonStringResponse);
            return;
        }

        Debug.Log(jsonStringResponse);
        TopPlayers.instance.SetTopPlayers(jsonStringResponse);
        UITopPlayers.instance.ShowTopPlayers();
    }
}
