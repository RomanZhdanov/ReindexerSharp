using Newtonsoft.Json.Serialization;

namespace ReindexerClient.Utils
{
    public class JsonLowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
