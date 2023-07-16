using Newtonsoft.Json;

namespace ReindexerClient.RxCore.Models
{
    public class StatusResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("response_code")]
        public int ResponseCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
