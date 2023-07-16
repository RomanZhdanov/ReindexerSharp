using Newtonsoft.Json;
using ReindexerClient.Utils;

namespace ReindexerClient.Extensions
{
    public static class ObjectExtensions
    {
        public static string SerializeToJson(this object obj)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new JsonLowercaseContractResolver();

            return JsonConvert.SerializeObject(obj, serializerSettings);
        }
    }
}
