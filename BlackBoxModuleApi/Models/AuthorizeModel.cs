using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackBoxModuleApi.Models
{
    public class AuthorizeModel
    {
        [JsonProperty(PropertyName = "oauth_consumer_key")]
        public string OAuthConsumerKey { get; set; }

        [JsonProperty(PropertyName = "oauth_consumer_secret")]
        public string OAuthConsumerSecret { get; set; }

        [JsonProperty(PropertyName = "store_base_url")]
        public string StoreBaseUrl { get; set; }

        [JsonProperty(PropertyName = "oauth_verifier")]
        public string OAuthVerifier { get; set; }
    }
}