using Newtonsoft.Json;

namespace ReindexerClient.RxCore.Models
{
    public class DSLQuery
    {
        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("req_total")]
        public string RequestTotal { get; set; }

        [JsonProperty("filters")]
        public FilterDef[] Filters { get; set; }

        [JsonProperty("select_functions")]
        public string[] SelectFunctions { get; set; }
    }

    public class FilterDef
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("cond")]
        public string Condition { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
