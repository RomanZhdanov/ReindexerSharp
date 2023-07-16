using Newtonsoft.Json;

namespace ReindexerClient.RxCore.Models
{
    public class FulltextConfig
    {
        [JsonProperty("enable_translit")]
        public bool EnableTranslit { get; set; }

        [JsonProperty("enable_numbers_search")]
        public bool EnableNumbersSearch { get; set; }

        [JsonProperty("enable_warmup_on_us_cop")]
        public bool EnableWarmupOnUsCopy { get; set; }

        [JsonProperty("enable_kb_layout")]
        public bool EnableKbLayout { get; set; }

        [JsonProperty("log_level")]
        public int LogLevel { get; set; }

        [JsonProperty("merge_limit")]
        public int MergeLimit { get; set; }

        [JsonProperty("extra_word_symbols")]
        public string ExtraWordSymbols { get; set; }

        [JsonProperty("stop_words")]
        public string[] StopWords { get; set; }

        [JsonProperty("stemmers")]
        public string[] Stemmers { get; set; }

        [JsonProperty("synonyms")]
        public FulltextSynonym[] Synonyms { get; set; }

        [JsonProperty("bm25_boost")]
        public float Bm25Boost { get; set; }

        [JsonProperty("m25_weight")]
        public float Bm25Weight { get; set; }

        [JsonProperty("distance_boost")]
        public float Distance_Boost { get; set; }

        [JsonProperty("distance_weight")]
        public float DistanceWeight { get; set; }

        [JsonProperty("term_len_boost")]
        public float TermLenBoost { get; set; }

        [JsonProperty("term_len_weight")]
        public float TermLenWeight { get; set; }

        [JsonProperty("position_boost")]
        public float PositionBoost { get; set; }

        [JsonProperty("position_weight")]
        public float PositionWeight { get; set; }

        [JsonProperty("full_match_boost")]
        public float FullMatchBoost { get; set; }

        [JsonProperty("partial_match_decrease")]
        public int PartialMatchDecrease { get; set; }

        [JsonProperty("min_relevancy")]
        public float MinRelevancy { get; set; }

        [JsonProperty("max_typos")]
        public int MaxTypos { get; set; }

        [JsonProperty("max_typo_len")]
        public int MaxTypoLen { get; set; }

        [JsonProperty("max_rebuild_steps")]
        public int MaxRebuildSteps { get; set; }

        [JsonProperty("max_step_size")]
        public int MaxStepSize { get; set; }

        [JsonProperty("sum_ranks_by_fields_ratio")]
        public float SumRanksByFieldsRatio { get; set; }

        [JsonProperty("fields")]
        public FulltextFieldConfig[] Fields { get; set; }
    }
}
