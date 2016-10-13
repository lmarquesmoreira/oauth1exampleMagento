using BlackBoxModuleApi.Cache;
using BlackBoxModuleApi.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackBoxModuleApi.Controllers
{
    public class OAuthCallbackController : ApiController
    {

        private static string CallbackUrl { get { return "https://blackboxmagentoapi.azurewebsites.net/api/OAuthCallback/checklogin"; } }

        [SwaggerOperation("PostFromMagento")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<HttpResponseMessage> Post([FromUri] string oauth_verifier = "", [FromUri] string oauth_consumer_key = "", [FromUri]string oauth_consumer_key_secret = "", [FromUri] string store_base_url = "")
        {

            try
            {
                var data = await Request.Content.ReadAsFormDataAsync();


                var model = new AuthorizeModel()
                {
                    OAuthConsumerKey = data.Get("oauth_consumer_key"),
                    OAuthConsumerSecret = data.Get("oauth_consumer_secret"),
                    OAuthVerifier = data.Get("store_base_url"),
                    StoreBaseUrl = data.Get("oauth_verifier")
                };


                Trace.TraceInformation($"Body Data : {model.OAuthConsumerKey} \n,  {model.OAuthConsumerSecret} \n,  {model.OAuthVerifier} \n,  {model.StoreBaseUrl}");

                CacheManager cache = new CacheManager();
                cache.Set("OAuthConsumerKey", model);

                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            // Cache de Request Data

            // Get Request Token
            // Get Access Token
        }

        [HttpGet]
        // get html form login
        public HttpResponseMessage Authorize()
        {
            var response = new HttpResponseMessage();

            CacheManager cache = new CacheManager();
            var model = cache.Get<AuthorizeModel>("OAuthConsumerKey");

            StringBuilder html = new StringBuilder();
            html.Append("<table width=\"300\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#CCCCCC\"><tr>");
            html.Append($"<form name=\"form1\" method=\"post\" action=\"{CallbackUrl}? oauth_consumer_key={model.OAuthConsumerKey}&callback_url={CallbackUrl}\">");
            html.Append("<td><table width=\"100%\" border=\"0\" cellpadding=\"3\" cellspacing=\"1\" bgcolor=\"#FFFFFF\"><tr><td colspan=\"3\"><strong>BlackBox Login</strong></td></tr><tr><td width=\"78\">Username</td><td width=\"6\">:</td><td width=\"294\"><input name=\"myusername\" type=\"text\" id=\"myusername\"></td></tr><tr><td>Senha</td><td>:</td><td><input name=\"mypassword\" type=\"text\" id=\"mypassword\"></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td><input type=\"submit\" name=\"Submit\" value=\"Login\"></td></tr></table></td></form></tr></table>");

            response.Content = new StringContent($"<html><body>{html.ToString()}</body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CheckLogin([FromUri]string oauth_consumer_key, [FromUri] string callback_url)
        {
            //suponha que os dados do login estão corretos

            CacheManager cache = new CacheManager();
            var model = cache.Get<AuthorizeModel>("OAuthConsumerKey");

            var url = model.StoreBaseUrl + "/oauth/token/request/";
            // Get RequestToken
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", $"oauth_consumer_key=\"{model.OAuthConsumerKey}\",oauth_signature_method=\"HMAC-SHA1\",oauth_signature=\"\",oauth_timestamp=\"{GenerateTimeStamp()}\",oauth_nonce=\"nonce\",oauth_version=\"1\"");
                await client.PostAsJsonAsync($"{url}", "");
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }

    public class OAuthParameters
    {
        public OAuthParameters(string clientId, string clientSecret)
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
    }
}
