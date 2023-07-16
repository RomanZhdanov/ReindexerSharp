using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReindexerClient.RxCore.Models
{
    public class DatabasesListResponse
    {
        [JsonProperty("total_items")]
        public int TotalItems { get; set; }

        [JsonProperty("items")]
        public List<string> Items { get; set; }
    }
}
