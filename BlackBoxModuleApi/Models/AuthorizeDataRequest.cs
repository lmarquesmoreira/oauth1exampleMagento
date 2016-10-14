using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Globalization;

namespace BlackBoxModuleApi.Models
{
    public class AuthorizeDataRequest
    {
        [JsonProperty(PropertyName = "oauth_consumer_key")]
        public string oauth_consumer_key { get; set; }

        [JsonProperty(PropertyName = "success_call_back")]
        public string success_call_back { get; set; }
    }
}