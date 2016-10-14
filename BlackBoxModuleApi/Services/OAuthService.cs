using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlackBoxModuleApi.Models;
using System.Diagnostics;
using System.Net;
using BlackBoxModuleApi.Cache;
using System.Text;
using System.Net.Http.Headers;
using System.Configuration;

namespace BlackBoxModuleApi.Services
{
    public class OAuthService : IOAuthService
    {

        protected readonly ICacheManager Cache;

        public string CallbackUrl { get { return $"{ConfigurationManager.AppSettings["ApiUrl"]}/api/oauth/checklogin"; } }

        public OAuthService(ICacheManager cache)
        {
            Cache = cache;
        }

        public HttpResponseMessage Authorize(AuthorizeDataRequest data)
        {
            var response = new HttpResponseMessage();

            //alterei callback_url para success_call_back @:
            
            StringBuilder html = new StringBuilder();
            html.Append("<table width=\"300\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#CCCCCC\"><tr>");
            html.Append($"<form name=\"form1\" method=\"post\" action=\"{CallbackUrl}? oauth_consumer_key={data.oauth_consumer_key}&success_call_back={data.success_call_back}\">");
            html.Append("<td><table width=\"100%\" border=\"0\" cellpadding=\"3\" cellspacing=\"1\" bgcolor=\"#FFFFFF\"><tr><td colspan=\"3\"><strong>BlackBox Login</strong></td></tr><tr><td width=\"78\">Username</td><td width=\"6\">:</td><td width=\"294\"><input name=\"myusername\" type=\"text\" id=\"myusername\"></td></tr><tr><td>Senha</td><td>:</td><td><input name=\"mypassword\" type=\"text\" id=\"mypassword\"></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td><input type=\"submit\" name=\"Submit\" value=\"Login\"></td></tr></table></td></form></tr></table>");

            response.Content = new StringContent($"<html><body>{html.ToString()}</body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        public async Task<HttpResponseMessage> CheckLogin(AuthorizeDataRequest data)
        {
            //suponha que os dados do login estão corretos...
            var model = Cache.Get<EndpointDataRequest>("OAuthConsumerKey");
            var url = model.store_base_url + "/oauth/token/request/";
            var timestamp = OAuthParams.GenerateTimeStamp();

            OAuthParams oauth = new OAuthParams(model.oauth_consumer_key, model.oauth_consumer_key_secret);
            oauth.GenerateSignature("nonce", timestamp, new Uri(data.success_call_back));

            // Get RequestToken
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"oauth_consumer_key=\"{data.oauth_consumer_key}\",oauth_signature_method=\"HMAC-SHA1\",oauth_signature=\"\",oauth_timestamp=\"{timestamp}\",oauth_nonce=\"nonce\",oauth_version=\"1.0\"");
                await client.PostAsJsonAsync($"{url}", "");
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Endpoint(HttpContent content)
        {
            try
            {
                var data = await content.ReadAsFormDataAsync();
                var model = new EndpointDataRequest()
                {
                    oauth_consumer_key = data.Get("oauth_consumer_key"),
                    oauth_consumer_key_secret = data.Get("oauth_consumer_secret"),
                    oauth_verifier = data.Get("store_base_url"),
                    store_base_url = data.Get("oauth_verifier")
                };

                Trace.TraceInformation($"Body Data : {model.oauth_consumer_key} \n,  {model.oauth_consumer_key_secret} \n,  {model.oauth_verifier} \n,  {model.store_base_url}");

                Cache.Set("OAuthConsumerKey", model);

                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);

                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}