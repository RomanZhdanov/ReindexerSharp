using Newtonsoft.Json;

namespace ReindexerClient.RxCore.Models
{
    public class Namespace
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("storage")]
        public InlineModel2 Storage { get; set; }

        [JsonProperty("indexes")]
        public Index[] Indexes { get; set; }
    }
}
