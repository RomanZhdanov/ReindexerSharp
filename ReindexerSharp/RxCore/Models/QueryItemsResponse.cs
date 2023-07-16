using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReindexerClient.RxCore.Models
{
    public class QueryItemsResponse<T>
    {
        [JsonProperty("items")]
        public IList<T> Items { get; set; }

        [JsonProperty("namespaces")]
        public string[] Namespaces { get; set; }

        [JsonProperty("cache_enabled")]
        public bool CacheEnabled { get; set; }

        [JsonProperty("query_total_items")]
        public int QueryTotalItems { get; set; }
    }
}
