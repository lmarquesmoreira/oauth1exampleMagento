using Newtonsoft.Json;

namespace BlackBoxModuleApi.Models
{
    public class EndpointDataRequest
    {
        [JsonProperty(PropertyName = "oauth_verifier")]
        public string oauth_verifier { get; set; }

        [JsonProperty(PropertyName = "oauth_consumer_key")]
        public string oauth_consumer_key { get; set; }

        [JsonProperty(PropertyName = "oauth_consumer_key_secret")]
        public string oauth_consumer_key_secret { get; set; }

        [JsonProperty(PropertyName = "store_base_url")]
        public string store_base_url { get; set; }
    }

}