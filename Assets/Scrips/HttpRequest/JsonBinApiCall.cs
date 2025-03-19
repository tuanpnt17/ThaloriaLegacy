using UnityEngine.Networking;

namespace Assets.Scrips.HttpRequest
{
    public class JsonBinApiCall : ApiCall
    {
        private readonly string _masterKey =
            "$2a$10$UkbLRijihRgTr3X/YEesOeBxtW80Il5.7jkEvgIXbyyqbfQEJVKd6";
        private readonly string _accessKey =
            "$2a$10$zqX4ldPj5.zSN4Pz3wQY0eEdA1IwxKTuvKOcic/J/7MIX4IDMGhaa";

        public override string Domain()
        {
            return "api.jsonbin.io/v3";
        }

        public override void SetHeaders(UnityWebRequest request)
        {
            base.SetHeaders(request);

            //TODO: set your own Master Key here
            request.SetRequestHeader("X-Master-Key", _masterKey);
            request.SetRequestHeader("X-Access-Key", _accessKey);
        }
    }
}
