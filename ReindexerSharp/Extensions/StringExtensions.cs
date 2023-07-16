using System.Net.Http;
using System.Text;

namespace ReindexerClient.Extensions
{
    public static class StringExtensions
    {
        public static StringContent ToJsonStringContent(this string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static string ToSafeQuery(this string query)
        {
            return query.Replace("=", "%3D");
        }
    }
}
