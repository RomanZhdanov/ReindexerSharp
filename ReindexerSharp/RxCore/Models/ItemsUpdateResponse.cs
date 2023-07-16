using Newtonsoft.Json;

namespace ReindexerClient.RxCore.Models
{
    public class ItemsUpdateResponse
    {
        [JsonProperty("updated")]
        public int Updated { get; set; }

        [JsonProperty("items")]
        public object[] Items { get; set; }
    }
}
