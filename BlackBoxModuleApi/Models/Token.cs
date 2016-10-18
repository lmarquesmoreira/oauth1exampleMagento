using Newtonsoft.Json;

namespace BlackBoxModuleApi.Models
{
    public class Token
    {
        [JsonProperty(PropertyName = "oauth_token")]
        public string oauth_token { get; set; }

        [JsonProperty(PropertyName = "oauth_token_secret")]
        public string oauth_token_secret { get; set; }
    }
}