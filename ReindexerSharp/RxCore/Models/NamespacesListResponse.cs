using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReindexerClient.RxCore.Models
{
    public class NamespacesListResponse
    {
        [JsonProperty("total_items")]
        public int TotalItems { get; set; }

        [JsonProperty("items")]
        public List<NsItem> Items { get; set; }
    }

    public class NsItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
