using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlackBoxModuleApi.Models
{
    public class OAuthParams
    {
        public OAuthParams(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        protected string NormalizeParameters(SortedDictionary<string, string> parameters)
        {
            StringBuilder sb = new StringBuilder();

            var i = 0;
            foreach (var parameter in parameters)
            {
                if (i > 0)
                    sb.Append("&");

                sb.AppendFormat("{0}={1}", parameter.Key, parameter.Value);

                i++;
            }

            return sb.ToString();
        }

        private string GenerateBase(string nonce, string timeStamp, Uri url)
        {
            var parameters = new SortedDictionary<string, string>
        {
            {"oauth_consumer_key", ClientId},
            {"oauth_signature_method", "HMAC-SHA1"},
            {"oauth_timestamp", timeStamp},
            {"oauth_nonce", nonce},
            {"oauth_version", "1.0"}
        };

            var sb = new StringBuilder();
            sb.Append("POST");
            sb.Append("&" + Uri.EscapeDataString(url.AbsoluteUri));
            sb.Append("&" + Uri.EscapeDataString(NormalizeParameters(parameters)));
            return sb.ToString();
        }

        public string GenerateSignature(string nonce, string timeStamp, Uri url)
        {
            var signatureBase = GenerateBase(nonce, timeStamp, url);
            var signatureKey = string.Format("{0}&{1}", ClientSecret, "");
            var hmac = new HMACSHA1(Encoding.ASCII.GetBytes(signatureKey));
            return Convert.ToBase64String(hmac.ComputeHash(new ASCIIEncoding().GetBytes(signatureBase)));
        }

        public static string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 1, 0, 0, 0, 0));
            string timeStamp = ts.TotalSeconds.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.IndexOf("."));
            return timeStamp;
        }
    }
}