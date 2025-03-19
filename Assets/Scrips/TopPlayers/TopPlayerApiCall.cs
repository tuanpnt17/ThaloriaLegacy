using Assets.Scrips.HttpRequest;

namespace Assets.Scrips.TopPlayers
{
    public class TopPlayerApiCall : JsonBinApiCall
    {
        protected string JsonId = "67da8c5d8a456b796678df97";

        public virtual string Uri()
        {
            return "/b/" + JsonId;
        }
    }
}
