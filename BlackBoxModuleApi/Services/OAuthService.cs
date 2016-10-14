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
using System.Collections.Generic;
using System.Web;

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
            html.Append($"<form name=\"form1\" method=\"post\" action=\"{CallbackUrl}?oauth_consumer_key={data.oauth_consumer_key}&success_call_back={data.success_call_back}\">");
            html.Append("<td><table width=\"100%\" border=\"0\" cellpadding=\"3\" cellspacing=\"1\" bgcolor=\"#FFFFFF\"><tr><td colspan=\"3\"><strong>BlackBox Login</strong></td></tr><tr><td width=\"78\">Username</td><td width=\"6\">:</td><td width=\"294\"><input name=\"myusername\" type=\"text\" id=\"myusername\"></td></tr><tr><td>Senha</td><td>:</td><td><input name=\"mypassword\" type=\"text\" id=\"mypassword\"></td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td><input type=\"submit\" name=\"Submit\" value=\"Login\"></td></tr></table></td></form></tr></table>");

            response.Content = new StringContent($"<html><body>{html.ToString()}</body></html>");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        public async Task<HttpResponseMessage> CheckLogin(AuthorizeDataRequest data)
        {
            //suponha que os dados do login estão corretos...
            try
            {
                if (data.oauth_consumer_key != null || data.success_call_back != null)
                {
                    Trace.TraceInformation($"[AuthorizeDataRequest] => key: {data.oauth_consumer_key}; callback: {data.success_call_back}");
                }

                var model = Cache.Get<EndpointDataRequest>("OAuthConsumerKey");

                Trace.TraceInformation($"[RequestToken_EndpoindDataRequest] => key: {model.oauth_consumer_key}; secret: {model.oauth_consumer_key_secret}");

                Trace.TraceInformation($"[store_base_url] => {model.store_base_url}");

                var url = model.store_base_url + "oauth/token/request/";
                var timestamp = OAuthParams.GenerateTimeStamp();
                var nonce = Guid.NewGuid().ToString();

                OAuthParams oauth = new OAuthParams(model.oauth_consumer_key, model.oauth_consumer_key_secret);
                var signature = oauth.GenerateSignature(nonce, timestamp, new Uri(model.store_base_url));

                Trace.TraceInformation($"[Signature] => {signature}");

                // Get RequestToken
                using (var client = new HttpClient())
                {
                    var authorize = $"OAuth oauth_consumer_key=\"{model.oauth_consumer_key}\", oauth_nonce=\"{nonce}\", oauth_signature=\"{signature}\", oauth_signature_method=\"HMAC-SHA1\", oauth_timestamp=\"{timestamp}\", oauth_version=\"1.0\"";

                    Trace.TraceInformation($"[Authorization] => {authorize}");
                    try
                    {
                        client.DefaultRequestHeaders.Add("Authorization", authorize);
                        var response = await client.PostAsJsonAsync($"{url}", "");
                        var s = await response.Content.ReadAsStringAsync();
                        Trace.TraceInformation($"[ResquestTokenResponse] => {s}");
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError($"[ResquestTokenResponse] => {e.Message}");
                        Trace.TraceError($"[ResquestTokenResponse] => {e.StackTrace}");
                    }
                }


                // Get AccessToken

            }
            catch (Exception e)
            {
                Trace.TraceError($"[RequestToken] => {e.Message}");
                Trace.TraceError($"[RequestToken] => {e.StackTrace}");
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
                    oauth_verifier = data.Get("oauth_verifier"),
                    store_base_url = data.Get("store_base_url")
                };

                try
                {
                    
                    var rTrace = "[Endpoint_RESPONSE] => ";
                    foreach (string key in data)
                        rTrace += $" {key} : {data[key]} ";
                    Trace.TraceInformation(rTrace);

                    var token = data.Get("oauth_token");
                    if (!string.IsNullOrEmpty(token))
                        Trace.TraceInformation($"[OAUTH_TOKEN] => {token}");
                    else
                        Trace.TraceError($"[OAUTH_TOKEN] => não existe token");
                }
                catch (Exception e)
                {
                    Trace.TraceError($"[OAUTH_TOKEN] => não existe token");
                }

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