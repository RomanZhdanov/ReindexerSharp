using Newtonsoft.Json;

namespace ReindexerClient.RxCore.Models
{
    public class FulltextSynonym
    {
        [JsonProperty("tokens")]
        public string[] Tokens { get; set; }

        [JsonProperty("alternatives")]
        public string[] Alternatives { get; set; }
    }
}
