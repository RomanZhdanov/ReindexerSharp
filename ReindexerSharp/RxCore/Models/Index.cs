using Newtonsoft.Json;
using ReindexerClient.RxCore.Enums;

namespace ReindexerClient.RxCore.Models
{
    public class Index
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("json_paths")]
        public string[] JsonPaths { get; set; }

        [JsonProperty("field_type")]
        public string FieldType { get; set; }

        [JsonProperty("index_type")]
        public string IndexType { get; set; } = IndexTypes.None;

        [JsonProperty("expire_after")]
        public int ExpireAfter { get; set; }

        [JsonProperty("is_pk")]
        public bool IsPk { get; set; }

        [JsonProperty("is_array")]
        public bool IsArray { get; set; }

        [JsonProperty("is_dense")]
        public bool IsDense { get; set; }

        [JsonProperty("is_sparse")]
        public bool IsSparse { get; set; }

        [JsonProperty("rtree_type")]
        public string RtreeType { get; set; } = RTreeTypes.Rstart;

        [JsonProperty("is_simple_tag")]
        public bool IsSimpleTag { get; set; }

        [JsonProperty("collate_mode")]
        public string CollateMode { get; set; } = CollateModes.None;

        [JsonProperty("sort_order_letters")]
        public string SortOrderLetters { get; set; }

        [JsonProperty("config")]
        public FulltextConfig Config { get; set; }
    }
}
