using Newtonsoft.Json;

namespace ReindexerClient.RxCore.Models
{
    public class FulltextFieldConfig
    {
        [JsonProperty("field_name")]
        public string FieldName { get; set; }

        [JsonProperty("bm25_boost")]
        public float Bm25Boost { get; set; }

        [JsonProperty("bm25_weight")]
        public float Bm25Weight { get; set; }

        [JsonProperty("term_len_boost")]
        public float TermLenBoost { get; set; }

        [JsonProperty("term_len_weight")]
        public float TermLenWeight { get; set; }

        [JsonProperty("position_boost")]
        public float PositionBoost { get; set; }

        [JsonProperty("position_weight")]
        public float PositionWeight { get; set; }
    }
}
